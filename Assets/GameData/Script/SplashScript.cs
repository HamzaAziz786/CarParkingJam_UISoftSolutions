using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScript : MonoBehaviour
{
    public bool ShowConsent;
    public string SceneName;
    public float DelayTime;
    public GameObject GdprPopUp, Logo;

    void Awake()
    {
        if (ShowConsent)
        {
            if (PlayerPrefs.GetInt("GdprAccepted") == 0)
            {
                GdprPopUp.SetActive(true);
                Logo.SetActive(false);
            }
            else
            {
                GdprPopUp.SetActive(false);
                Logo.SetActive(true);
                StartCoroutine(Load());
                //AdsManagerWrapper.Instance.initialize(true);
            }
        }
        else
        {
            GdprPopUp.SetActive(false);
            Logo.SetActive(true);
            StartCoroutine(Load());
        }

        
    }

    public void GDRP_yes()
    {
        GdprPopUp.SetActive(false);
        Logo.SetActive(true);
        PlayerPrefs.SetInt("GdprAccepted", 1);
        //AdsManagerWrapper.Instance.initialize(true);
        StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        yield return new WaitForSeconds(DelayTime);
        NextSceneLoad();
    }

    public void NextSceneLoad()
    {
        SceneManager.LoadScene(SceneName);
    }
}
