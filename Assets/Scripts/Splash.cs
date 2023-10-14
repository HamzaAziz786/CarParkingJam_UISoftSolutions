using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
    public GameObject LoadingPanel;
    public GameObject PrivacyPanel;
    public Image LodingImg;
    private AsyncOperation _asyncLoad;

    private void Start()
    {

       


        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        if(PlayerPrefs.GetInt("Once") == 0)
        {
            PrivacyPanel.SetActive(true);

        }else
        {
            StartCoroutine(ActiveLoading());
        }
        Firebase_Analytics.Instance.LogEvent("Splash");
    }

    public void SubmitBtnClick()
    {
        PlayerPrefs.SetInt("Once", 1);
        StartCoroutine(ActiveLoading());
        PrivacyPanel.SetActive(false);
    }
    
    public void PolicyLinkOpen()
    {
        Application.OpenURL("https://sites.google.com/view/parkinggamesarena/home");
    }


    private IEnumerator ActiveLoading()
    {
        yield return new WaitForSeconds(0f);

        PrivacyPanel.SetActive(false);
        LoadingPanel.SetActive(true);
        StartCoroutine(LoadYourAsyncScene());
    }

    private IEnumerator LoadYourAsyncScene()
    {
        yield return new WaitForSecondsRealtime(1f);
        _asyncLoad = SceneManager.LoadSceneAsync("MainMenu");

        // Wait until the asynchronous scene fully loads
        while (!_asyncLoad.isDone)
        {
            yield return null;
        }
    }

    private void Update()
    {
        if (LoadingPanel.activeSelf && _asyncLoad != null)
        {
            LodingImg.fillAmount = _asyncLoad.progress;
        }
    }
}
