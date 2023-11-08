using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.UIElements.Experimental;

public class MenuManager : MonoBehaviour
{
    [Space(5)]
    [Header("Panels")]
    public GameObject MainPanel;
    public GameObject ModePanel;
    public GameObject SettingsPanel;
    public GameObject ShopPanel;
    public GameObject LoadingPanel;
    public GameObject QuitPanel;

    public Image LoadingImg;
    private AsyncOperation _asyncLoad;
    public AudioSource BtnClickSource;
    public AudioClip BtnClip;

    [Space(5)]
    [Header("Extra")]
    //public AudioSource Music;
    public static int LevelNum, LevelNum2;
    public Text[] CashTxt;
    public static MenuManager instance;
    public Slider vol;
    public GameObject ComingSoon;
    public static bool ParkingJam;
    private void Start()
    {
        Time.timeScale = 1;
        instance = this;
        AdsController.instance.ShowAd(AdType.BANNER, 0);
        if (PlayerPrefs.GetInt("once1") == 0)
        {

            PlayerPrefs.SetInt("sound", 0);

            PlayerPrefs.SetFloat("volume", 1);

            PlayerPrefs.SetInt("once1", 1);

        }

        AudioListener.volume = PlayerPrefs.GetFloat("volume");

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Firebase_Analytics.Instance.LogEvent("MainMenu");

    }


    private void HidePanels()
    {
        MainPanel.SetActive(false);
        ModePanel.SetActive(false);
        SettingsPanel.SetActive(false);
        ShopPanel.SetActive(false);
        LoadingPanel.SetActive(false);
        QuitPanel.SetActive(false);
    }

    public void BtnClickSound()
    {
      //  if (PlayerPrefs.GetInt("sound") == 0)
            BtnClickSource.PlayOneShot(BtnClip);
    }
    
    
    //---------Main Panel Buttons Functions---------------------------------------------------------------
    public void OnPlayBtnClick()
    {
        BtnClickSound();
        HidePanels();
        ModePanel.SetActive(true);
    }
    public void OnShopBtnClick()
    {
        BtnClickSound();
        HidePanels();
        ShopPanel.SetActive(true);
    }
    public void OnSettingBtnClick()
    {
        BtnClickSound();
        HidePanels();
        SettingsPanel.SetActive(true);
    }

    public void RateUs()
    {
        BtnClickSound();
        Application.OpenURL("https://play.google.com/store/apps/dev?id=" + Application.identifier);
    }

    //---------Back Btn Functions------------------------------------------------------------------------
    #region

    public void BackBtnFun()
    {
        BtnClickSound();
        if (!SettingsPanel.activeSelf)
        {
            if (MainPanel.activeSelf && !ShopPanel.activeSelf)
                BackFromMainPanel();
            else if (QuitPanel.activeSelf)
                BackFromQuitPanel();
            else if (ShopPanel.activeSelf)
                BackFromShop();
            else if (ModePanel.activeSelf)
                BackFromModePanel();
        }
        else
            BackFromSettings();
    }

    private void BackFromMainPanel()
    {
        HidePanels();
        QuitPanel.SetActive(true);

    }

    private void BackFromModePanel()
    {
        HidePanels();
        MainPanel.SetActive(true);
        ParkingJam = false;
    }

    private void BackFromSettings()
    {
        HidePanels();
        MainPanel.SetActive(true);
    }

    private void BackFromShop()
    {
        HidePanels();
        MainPanel.SetActive(true);

    }

    private void BackFromQuitPanel()
    {
        HidePanels();
        MainPanel.SetActive(true);

    }

    #endregion

    //------------------------------------------------------------------------------------

    public void OnQuitBtnClick()
    {
        Application.Quit();
    }

    public void OnLevelNextBtnClick()
    {
        BtnClickSound();
    }

    public void InApp_Purchase(string str)
    {
        BtnClickSound();
    }
    
    public void SelectParkingJamMode()
    {
        BtnClickSound();
        HidePanels();
        LoadingPanel.SetActive(true);
        ParkingJam = true;

        StartCoroutine(LoadYourAsyncScene());
    }

    private IEnumerator LoadYourAsyncScene()
    {
        yield return new WaitForSeconds(9f);
        if(ParkingJam)
            _asyncLoad = SceneManager.LoadSceneAsync("ParkingJam");

        while (!_asyncLoad.isDone)
        {
            yield return null;
        }
        Firebase_Analytics.Instance.LogEvent("PlayGame");
    }
    public void PlayChallenges()
    {
        Firebase_Analytics.Instance.LogEvent("ChallengesMode");
    }
    private void Update()
    {

        if (LoadingPanel.activeSelf && _asyncLoad != null)
            LoadingImg.fillAmount = Mathf.Lerp(LoadingImg.fillAmount, _asyncLoad.progress, Time.deltaTime);

        foreach (var txt in CashTxt)
        {
            txt.text = "" + PlayerPrefs.GetInt("cash");
        }

        if (!SettingsPanel.activeSelf) return;
        AudioListener.volume = vol.value;
        PlayerPrefs.SetFloat("volume", vol.value);
    }
    
    public void WatchVideo()
    {
        BtnClickSound();
    }
    public void ClosePanel(GameObject Panel)
    {
        Panel.transform.DOScale(0, .2f).OnComplete(() => PanelOFF(Panel)).SetEase(Ease.Linear); 
    }

    public void PanelOFF(GameObject paneloff)
    {
       // paneloff.SetActive(false);
    }
    public void OpenPanel(GameObject Panel)
    {
        Panel.transform.DOScale(1, .2f).OnComplete(() => PanelON(Panel)).SetEase(Ease.Linear);
    }

    public void PanelON(GameObject paneloff)
    {
       // paneloff.SetActive(true);
    }
    public void ComingSoonOn()
    {
       
        ComingSoon.transform.DOScale(1, .2f).SetEase(Ease.OutBounce);
        Invoke(nameof(BackComingSoon), .7f);
    }
    public void BackComingSoon()
    {
        ComingSoon.transform.DOScale(0, .2f).SetEase(Ease.InBounce);
    }
}
