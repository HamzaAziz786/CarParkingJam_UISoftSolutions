using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Space(5)]
    [Header("Panels")]
    public GameObject MainPanel;
    public GameObject ModePanel;
    public GameObject LevelPanel;
    public GameObject LevelPanel_Driving;
    public GameObject SettingsPanel;
    public GameObject StorePanel;
    public GameObject ShopPanel;
    public GameObject LoadingPanel;
    public GameObject QuitPanel;

    public Image LoadingImg;
    AsyncOperation asyncLoad;
    public AudioSource BtnClickSource;
    public AudioClip BtnClip;

    [Space(5)]
    [Header("Extra")]
    bool IsGarage;
    //public AudioSource Music;
    public static bool IsNext;
    public static int LevelNum, LevelNum2;
    public Text[] CashTxt;
    public static MenuManager instance;
    public Slider vol;
    public static bool ParkingMode, DrivingMode, ParkingJam;
    private void Start()
    {
        Time.timeScale = 1;
        instance = this;



        if (PlayerPrefs.GetInt("once1") == 0)
        {

            PlayerPrefs.SetInt("sound", 0);

            PlayerPrefs.SetFloat("volume", 1);

            PlayerPrefs.SetInt("once1", 1);

        }

        if (IsNext)
        {

            HidePanels();
            LevelPanel.SetActive(true);

            IsNext = false;
        }

        AudioListener.volume = PlayerPrefs.GetFloat("volume");

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        ShowBanner();

    }


    public void HidePanels()
    {
        MainPanel.SetActive(false);
        ModePanel.SetActive(false);
        LevelPanel_Driving.SetActive(false);
        LevelPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        StorePanel.SetActive(false);
        ShopPanel.SetActive(false);
        LoadingPanel.SetActive(false);
        QuitPanel.SetActive(false);
    }


    public void OnGarageBtnClick()
    {
        BtnClickSound();
        IsGarage = true;
        HidePanels();
        StorePanel.SetActive(true);
    }

    public void BtnClickSound()
    {
        if (PlayerPrefs.GetInt("sound") == 0)
            BtnClickSource.PlayOneShot(BtnClip);
    }
    //---------Main Panel Buttons Functions---------------------------------------------------------------
    public void OnPlayBtnClick()
    {
        BtnClickSound();
        HidePanels();
        StorePanel.SetActive(true);

        ShowBanner();
        ShowInterstitial();
    }
    public void OnShopBtnClick()
    {
        BtnClickSound();
        //HidePanels();
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
        Application.OpenURL("https://play.google.com/store/apps/details?id=" + Application.identifier);
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
            else if (LevelPanel.activeSelf || LevelPanel_Driving.activeSelf)
                BackFromLevelPanel();
            else if (ShopPanel.activeSelf)
                BackFromShop();
            else if (StorePanel.activeSelf)
                BackFromStorePanel();
            else if (ModePanel.activeSelf)
                BackFromModePanel();
        }
        else
            BackFromSettings();
    }

    void BackFromMainPanel()
    {
        HidePanels();
        QuitPanel.SetActive(true);

    }

    void BackFromLevelPanel()
    {
        HidePanels();
        ModePanel.SetActive(true);

        ParkingMode = false;
        DrivingMode = false;
        ParkingJam = false;
    }
    void BackFromModePanel()
    {
        HidePanels();
        StorePanel.SetActive(true);

        ParkingMode = false;
        DrivingMode = false;
        ParkingJam = false;
    }
    void BackFromSettings()
    {
        HidePanels();
        MainPanel.SetActive(true);
    }
    void BackFromShop()
    {
        //HidePanels();
        ShopPanel.SetActive(false);
        //MainPanel.SetActive(true);
    }

    void BackFromStorePanel()
    {
        HidePanels();
        MainPanel.SetActive(true);
    }

    void BackFromQuitPanel()
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


    public void SelectLevel(int num)
    {
        BtnClickSound();
        LevelNum = num;

        HidePanels();
        LoadingPanel.SetActive(true);
        StartCoroutine(LoadYourAsyncScene());

        ShowInterstitial();
    }
    public void SelectLevel_Driving(int num)
    {
        BtnClickSound();
        LevelNum2 = num;

        HidePanels();
        LoadingPanel.SetActive(true);
        StartCoroutine(LoadYourAsyncScene());

        ShowInterstitial();
    }

    public void OnLevelNextBtnClick()
    {
        BtnClickSound();

    }

    public void InApp_Purchase(string str)
    {
        BtnClickSound();
       // Purchaser.instance.BuyProduct(str);
    }

    public void OnStorePlayBtnClick()
    {
        BtnClickSound();
        HidePanels();
        ModePanel.SetActive(true);
    }
    public void SelectParkingMode()
    {
        BtnClickSound();
        HidePanels();
        LevelPanel.SetActive(true);

        ParkingMode = true;
        DrivingMode = false;
        ParkingJam = false;
    }
    public void SelectDrivingMode()
    {
        BtnClickSound();
        HidePanels();
        LevelPanel_Driving.SetActive(true);

        ParkingMode = false;
        DrivingMode = true;
        ParkingJam = false;
    }
    public void SelectFreeMode()
    {
        BtnClickSound();
        HidePanels();
        LoadingPanel.SetActive(true);

        ParkingMode = false;
        DrivingMode = false;

        StartCoroutine(LoadYourAsyncScene());

        ShowInterstitial();
    }
    public void SelectParkingJamMode()
    {
        BtnClickSound();
        HidePanels();
        LoadingPanel.SetActive(true);

        ParkingMode = false;
        DrivingMode = false;
        ParkingJam = true;

        StartCoroutine(LoadYourAsyncScene());

        ShowInterstitial();
    }
    IEnumerator LoadYourAsyncScene()
    {
        if(ParkingMode)
            asyncLoad = SceneManager.LoadSceneAsync("GamePlay");
        else if(DrivingMode)
            asyncLoad = SceneManager.LoadSceneAsync("GamePlay2");
        else if(ParkingJam)
            asyncLoad = SceneManager.LoadSceneAsync("ParkingJam");
        else
            asyncLoad = SceneManager.LoadSceneAsync("GamePlay2");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    private void Update()
    {

        if (LoadingPanel.activeSelf && asyncLoad != null)
            LoadingImg.fillAmount = Mathf.Lerp(LoadingImg.fillAmount, asyncLoad.progress, Time.deltaTime);

        foreach (Text txt in CashTxt)
        {
            txt.text = "" + PlayerPrefs.GetInt("cash");
        }

        if (SettingsPanel.activeSelf)
        {
            AudioListener.volume = vol.value;
            PlayerPrefs.SetFloat("volume", vol.value);
        }
    }


    //-------------------------------------------------------------

    public void ShowBanner()
    {
        if (PlayerPrefs.GetInt("removeads") == 0)
        {
            //AdsManager.Instance.ShowBannerAd();
        }
    }

    public void HideBanner()
    {
        if (PlayerPrefs.GetInt("removeads") == 0)
        {
           // AdsManager.Instance.HideBannerAd();
        }
    }

    public void ShowInterstitial()
    {
        if (PlayerPrefs.GetInt("removeads") == 0)
        {
            //AdsManager.Instance.ShowInterstitialLoading();
        }
    }



    public void WatchVideo()
    {
        BtnClickSound();
       // AdsManager.Instance.ShowRewardedVideoAd();
    }
}
