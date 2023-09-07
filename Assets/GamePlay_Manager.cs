using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlay_Manager : MonoBehaviour
{
    public GameObject GamePlayPanel, PausePanel, FailPanel, CompletePanel, loadingPanel;
    [Space]
    public GameObject RaceToturial;
    public GameObject GearToturial;
    public GameObject GearUp, GearDown;

    [Space]
    public GameObject[] Levels;

    [Space]
    public GameObject[] Players;

    [Space]
    public GameObject ReverseCamImg;
    Camera ReverseCam;
    public GameObject RewardDoubleBtn;
    public Text RewardTxt;
    public Slider vol;
    public static GamePlay_Manager Instance;
    public GameObject StarEffect;
    public Canvas MainCanvas;
    public AudioSource BtnClickSource;
    public AudioClip BtnClip, victoryClip, AlarmClip;
    public Text[] CashTxt;
    int num;

    private void Start()
    {
        Instance = this;
        Time.timeScale = 1;

        RCC_Settings.Instance.behaviorSelectedIndex = 3;

        MainCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

        HidePanels();
        GamePlayPanel.SetActive(true);

        Levels[MenuManager.LevelNum].SetActive(true);
        Players[PlayerPrefs.GetInt("currentPlayer")].SetActive(true);

        if(MenuManager.LevelNum == 0)
        {
            //GearToturial.SetActive(false);
            //RaceToturial.SetActive(true);
        }
        else if (MenuManager.LevelNum == 1)
        {
            GearToturial.SetActive(true);
            RaceToturial.SetActive(false);
        }

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        ReverseCam = GameObject.FindGameObjectWithTag("reverse").GetComponent<Camera>();

        vol.value = AudioListener.volume = PlayerPrefs.GetFloat("volume");
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        num = MenuManager.LevelNum;
        num++;
        AdsManager.Instance.LogEvent("level_" + num + "_started");
        //ShowBanner();
    }
    public void BtnClickSound()
    {
        if (PlayerPrefs.GetInt("sound") == 0)
            BtnClickSource.PlayOneShot(BtnClip);
    }
    private void Update()
    {
        if (PausePanel.activeSelf)
        {
            AudioListener.volume = vol.value;
            PlayerPrefs.SetFloat("volume", vol.value);
        }

        foreach (Text txt in CashTxt)
        {
            txt.text = "" + PlayerPrefs.GetInt("cash");
        }
    }

    public void ChangeTimeScale(float t)
    {
        Time.timeScale = t;
    }

    public void HidePanels()
    {
        GamePlayPanel.SetActive(false);
        PausePanel.SetActive(false);
        FailPanel.SetActive(false);
        CompletePanel.SetActive(false);
        loadingPanel.SetActive(false);
    }

    public void ReverseON()
    {
        BtnClickSound();
        ReverseCamImg.SetActive(true);
        ReverseCam.enabled = true;
    }
    public void ReverseOFF()
    {
        BtnClickSound();
        ReverseCamImg.SetActive(false);
        ReverseCam.enabled = false;
    }

    public void OnFail( )
    {
        Time.timeScale = 0;
        HidePanels();
        FailPanel.SetActive(true);
        AudioListener.volume = 0;

        AdsManager.Instance.LogEvent("level_" + num + "_failed");
        ShowInterstitial();
    }
    public void OnComplete()
    {
        MainCanvas.renderMode = RenderMode.ScreenSpaceCamera;

        StarEffect.SetActive(true);
        //Time.timeScale = 0;
        HidePanels();
        CompletePanel.SetActive(true);
        //AudioListener.volume = 0;

        PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") + 1000);

        if (PlayerPrefs.GetInt("levels") < 29 && MenuManager.LevelNum == PlayerPrefs.GetInt("levels"))
        {
            PlayerPrefs.SetInt("levels", PlayerPrefs.GetInt("levels") + 1);
        }
        AdsManager.Instance.LogEvent("level_" + num + "_completed");
    }
    public void Restart()
    {
        BtnClickSound();
        HidePanels();
        loadingPanel.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OnHome()
    {
        BtnClickSound();
        HidePanels();
        loadingPanel.SetActive(true);
        SceneManager.LoadScene("MainMenu");
    }
    public void OnLevels()
    {
        BtnClickSound();
        HidePanels();
        loadingPanel.SetActive(true);
        MenuManager.IsNext = true;
        SceneManager.LoadScene("MainMenu");
    }
    public void OnPause()
    {
        BtnClickSound();
        HidePanels();
        PausePanel.SetActive(true);
        Time.timeScale = 0;
        AudioListener.volume = 0;

        ShowInterstitial();
    }
    public void OnResume()
    {
        BtnClickSound();
        HidePanels();
        GamePlayPanel.SetActive(true);
        Time.timeScale = 1f;

        AudioListener.volume = PlayerPrefs.GetFloat("volume");
    }

    public void OnRewardDouble()
    {
        RewardTxt.text = "2000";
        RewardDoubleBtn.SetActive(false);
    }

    public void OnNext()
    {
        BtnClickSound();
        HidePanels();
        loadingPanel.SetActive(true);
        StartCoroutine(NextPlz());
        ShowInterstitial();

       
    }
    IEnumerator NextPlz()
    {
        yield return new WaitForSecondsRealtime(2f);
        if (MenuManager.LevelNum < 28)
        {
            MenuManager.LevelNum++;
            
        }
        else
        {
            MenuManager.LevelNum = 0;
           
        }
        Restart();
    }
    //-------------------------------------------------------------

    public void ShowBanner()
    {
        if (PlayerPrefs.GetInt("removeads") == 0)
        {
            AdsManager.Instance.ShowBannerAd();
        }
    }

    public void HideBanner()
    {
        if (PlayerPrefs.GetInt("removeads") == 0)
        {
            AdsManager.Instance.HideBannerAd();
        }
    }

    public void ShowInterstitial()
    {
        if (PlayerPrefs.GetInt("removeads") == 0)
        {
            AdsManager.Instance.ShowInterstitialLoading();
        }
    }

    public void WatchVideo()
    {
        AdsManager.Instance.ShowRewardedVideoAd();
    }
}
