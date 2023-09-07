using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
    public GameObject LoadingPanel;
    public GameObject PrivacyPanel;
    public Image LodingImg;
    AsyncOperation asyncLoad;

    private void Start()
    {

        Screen.orientation = ScreenOrientation.LandscapeLeft;


        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        if(PlayerPrefs.GetInt("Once") == 0)
        {
            PrivacyPanel.SetActive(true);

        }else
        {
            StartCoroutine(ActiveLoading());
        }

       

    }

    public void SubmitBtnClick()
    {
        PlayerPrefs.SetInt("Once", 1);
        StartCoroutine(ActiveLoading());
        PrivacyPanel.SetActive(false);
    }




    public void PolicyLinkOpen()
    {
        Application.OpenURL("https://sites.google.com/view/offline-games-production/home");
    }


    IEnumerator ActiveLoading()
    {
        yield return new WaitForSeconds(0f);

        PrivacyPanel.SetActive(false);
        LoadingPanel.SetActive(true);
        StartCoroutine(LoadYourAsyncScene());
    }
    IEnumerator LoadYourAsyncScene()
    {
        yield return new WaitForSecondsRealtime(1f);
        asyncLoad = SceneManager.LoadSceneAsync("MainMenu");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    private void Update()
    {
        if (LoadingPanel.activeSelf && asyncLoad != null)
        {
            LodingImg.fillAmount = asyncLoad.progress;
        }
    }
}
