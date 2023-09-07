using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gameplay : MonoBehaviour
{
    public GameObject completePanel, failPanel, pausePanel, GameplayPanel,signMessage,skip, loadingPanel;
    public GameObject[] players;
    public GameObject[] levels;


    public bool isOnce, failedBool = false;
    public GameObject fadeOutImg;
    Coroutine co;
    public RCC_CarControllerV3 myPlayer;
    public Canvas Rcc_Canvas;
    public GameObject SideImgsAnim;
    //public Text CompletePanelScoreTxt;
    public RCC_Camera rccCam;
    public Camera RccCamera;
    //public GameObject[] animCam;
    public GameObject[] animCam2;
    public static Gameplay instance;
    public Animator Notification;
    public Text sentences;
    //[HideInInspector] public Camera ActionCam;

    public AudioSource BtnClickSource;
    public AudioClip BtnClickSound, EngineStartSnd;
    public Image MissionImg;
    public Sprite[] MissionSprites;
    //public GameObject[] ArrowPointers;
    public GameObject Confetti, AmbientSound;
    //public Text LvlNo;
    //int lvlno;
    public GameObject GearUp, GearDown;
    Vector3 TempPos, TempRot;
    public GameObject StarEffect;
    public Text[] CashTxt;
    public Slider vol;
    void Awake()
    {

        instance = this;

        RCC_Settings.Instance.behaviorSelectedIndex = 0;

        if (MenuManager.DrivingMode)
        {
            sentence();
            levels[MenuManager.LevelNum2].SetActive(true);
            animCam2[MenuManager.LevelNum2].SetActive(true);
            skip.SetActive(true);
            //lvlno = MenuManager.LevelNum2;
            //lvlno++;
        }

        //LvlNo.text = "Level No. " + lvlno;

        FB_Events("started");
        players[PlayerPrefs.GetInt("currentPlayer")].SetActive(true);
        //ActionCam = GameObject.FindGameObjectWithTag("ActionCam").GetComponent<Camera>();

        Screen.orientation = ScreenOrientation.LandscapeLeft;
        //AdsManager.Instance.RequestBannerAd();
        ShowBanner();
        HideRect();
    }

    void FB_Events(string str)
    {
       if (MenuManager.DrivingMode)
        {
            int num = MenuManager.LevelNum2;
            num++;
            AdsManager.Instance.LogEvent("school_level_" + num + "_" + str);
        }
        
    }

    void Start()
    {

        hidePanels();
        myPlayer = players[PlayerPrefs.GetInt("currentPlayer")].GetComponent<RCC_CarControllerV3>();
        Rcc_Canvas.renderMode = RenderMode.ScreenSpaceOverlay;

       
        if (MenuManager.DrivingMode)
        {
                levels[MenuManager.LevelNum2].SetActive(true);
                StartCoroutine(waitForBusTransform());
            players[PlayerPrefs.GetInt("currentPlayer")].transform.position = PickPoint.instance.Destination.position;
            players[PlayerPrefs.GetInt("currentPlayer")].transform.rotation = PickPoint.instance.Destination.rotation;
        }else
        {
            skip.SetActive(false);
            GameplayPanel.SetActive(true);
        }


        fadeOut();
        //hidePanels();

        
        Time.timeScale = 1f;

        if (PlayerPrefs.GetInt("sound") == 0)
        {
            AudioListener.volume = 1f;
        }else
        {
            AudioListener.volume = 0;
        }

        
            RCC_Settings.Instance.mobileController = RCC_Settings.MobileController.SteeringWheel;
        

        StartCoroutine(delay());

        ShowBanner();
        HideRect();
    }

    void Update()
    {
        if (pausePanel.activeSelf)
        {
            AudioListener.volume = vol.value;
            PlayerPrefs.SetFloat("volume", vol.value);
        }
        foreach (Text txt in CashTxt)
        {
            txt.text = "" + PlayerPrefs.GetInt("cash");
        }
    }

    IEnumerator delay()
    {
        yield return new WaitForSecondsRealtime(2f);
        TempPos = players[PlayerPrefs.GetInt("currentPlayer")].transform.position;
        TempRot = players[PlayerPrefs.GetInt("currentPlayer")].transform.eulerAngles;
    }
    public void Respawn()
    {
        OnBtnClickSound();
        fadeOut();
        RCC_SceneManager.Instance.activePlayerVehicle.rigid.isKinematic = true;

        players[PlayerPrefs.GetInt("currentPlayer")].transform.position = TempPos;
        players[PlayerPrefs.GetInt("currentPlayer")].transform.eulerAngles = TempRot;
        StartCoroutine(UnFreez());
    }
    IEnumerator UnFreez()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        RCC_SceneManager.Instance.activePlayerVehicle.rigid.isKinematic = false;
    }
    public void Test_Next()
    {
        MenuManager.LevelNum2++;
        restart();
    }

   
    public void OnBtnClickSound()
    {
        if (PlayerPrefs.GetInt("sound") == 0)
        {
            BtnClickSource.PlayOneShot(BtnClickSound);
        }
    }
    public void ShowNotification()
    {
        Notification.SetBool("show", true);
        StartCoroutine(HideNotification());
    }
    IEnumerator HideNotification()
    {
        yield return new WaitForSecondsRealtime(2f);
        Notification.SetBool("show", false);
    }
    public void fadeOut()
    {
        fadeOutImg.SetActive(false);
        fadeOutImg.SetActive(true);
    }

    public void StartGame()
    {
        OnBtnClickSound();
        hidePanels();
        GameplayPanel.SetActive(true);
    }

    public void hidePanels()
    {
        GameplayPanel.SetActive(false);
        //missionPanel.SetActive(false);
        signMessage.SetActive(false);
        pausePanel.SetActive(false);
        failPanel.SetActive(false);
        completePanel.SetActive(false);
        loadingPanel.SetActive(false);
        
    }
    public void restart()
    {
        OnBtnClickSound();
        hidePanels();
        loadingPanel.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }
    public void changeControl()
    {
        OnBtnClickSound();
        if (RCC_Settings.Instance.mobileController == RCC_Settings.MobileController.TouchScreen)
        {
            RCC_Settings.Instance.mobileController = RCC_Settings.MobileController.SteeringWheel;
        }
        else if (RCC_Settings.Instance.mobileController == RCC_Settings.MobileController.SteeringWheel)
        {
            RCC_Settings.Instance.mobileController = RCC_Settings.MobileController.Gyro;
        }
        else if (RCC_Settings.Instance.mobileController == RCC_Settings.MobileController.Gyro)
        {
            RCC_Settings.Instance.mobileController = RCC_Settings.MobileController.TouchScreen;
        }
    }

    public void Control(int i)
    {
        OnBtnClickSound();
        if(i == 1)
        {
            RCC_Settings.Instance.mobileController = RCC_Settings.MobileController.Gyro;
        }
        if (i == 2)
        {
            RCC_Settings.Instance.mobileController = RCC_Settings.MobileController.SteeringWheel;
        }
        if (i == 3)
        {
            RCC_Settings.Instance.mobileController = RCC_Settings.MobileController.TouchScreen;
        }
       
    }


    public void EngineOn()
    {
        //OnBtnClickSound();
        BtnClickSource.PlayOneShot(EngineStartSnd);
        myPlayer.engineRunning = true;
        ShowBanner();
    }

    public void onPause()
    {
        OnBtnClickSound();
        hidePanels();
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        AudioListener.volume = 0;

        ShowInterstitial();
    }
    public void onResume()
    {
        OnBtnClickSound();
        hidePanels();
        GameplayPanel.SetActive(true);
        Time.timeScale = 1f;

        AudioListener.volume = PlayerPrefs.GetFloat("volume");
    }

    public void onNext()
    {
        OnBtnClickSound();
        hidePanels();
        loadingPanel.SetActive(true);

        StartCoroutine(NextPlz());
        ShowInterstitial();

    }

    IEnumerator NextPlz()
    {
        yield return new WaitForSecondsRealtime(2f);
        if (MenuManager.LevelNum2 < 18)
        {
            MenuManager.LevelNum2++;

        }
        else
        {
            MenuManager.LevelNum2 = 0;
        }

        restart();
    }
    public void onMenu()
    {
        OnBtnClickSound();
        hidePanels();
        loadingPanel.SetActive(true);
        SceneManager.LoadScene("MainMenu");

    }

   
    public void OnComplete()
    {
        AmbientSound.SetActive(false);
        Confetti.SetActive(true);
        
        hidePanels();
        StartCoroutine(Completed());
        
        FB_Events("completed");
    }

    public IEnumerator Completed()
    {
        yield return new WaitForSecondsRealtime(3f);
        Rcc_Canvas.renderMode = RenderMode.ScreenSpaceCamera;
        hidePanels();
        //players[PlayerPrefs.GetInt("currentPlayer")].SetActive(false);
        //Time.timeScale = 0f;
     
        StarEffect.SetActive(true);
        completePanel.SetActive(true);

        PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") + 1000);

        
        if (MenuManager.DrivingMode)
            
        {
            if (PlayerPrefs.GetInt("levels2") < 19 && MenuManager.LevelNum2 == PlayerPrefs.GetInt("levels2"))
            {
                PlayerPrefs.SetInt("levels2", PlayerPrefs.GetInt("levels2") + 1);
            }
        }

        

        ShowRect();
        ShowInterstitial();
    }

    public void OnFailed()
    {
        fadeOut();
        hidePanels();
        //miniMapCanvas.enabled = false;
        //failEffectPanel.SetActive(true);
    }

    public void Failed()
    {
        hidePanels();
        failPanel.SetActive(true);
        Time.timeScale = 0f;
       
        ShowRect();
        ShowInterstitial();
    }

    IEnumerator OnFail()
    {
        hidePanels();
        //miniMapCanvas.enabled = false;
        //continuePanel.SetActive(true);
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(4.5f);
        //Failed();
    }

    public void changeCameraView()
    {
        OnBtnClickSound();
        if (rccCam.cameraMode == RCC_Camera.CameraMode.TPS)
        {
           
            rccCam.cameraMode = RCC_Camera.CameraMode.WHEEL;
            
        }
        else if (rccCam.cameraMode == RCC_Camera.CameraMode.WHEEL)
        {
           
            rccCam.cameraMode = RCC_Camera.CameraMode.TPS;
           
        }
       

    }


    public void sentence()
    {

        MissionImg.sprite = MissionSprites[MenuManager.LevelNum2];

        if (MenuManager.LevelNum2 == 0)
        {
            sentences.text = "A Single Solid Center Line Shows the Center of two way road. You are not to cross in either direction on roads" +
                " marked in this manner.";
        }

        if (MenuManager.LevelNum2 == 1)
        {
            sentences.text = "This is the Stop Signal. When you see this sign on the road you must stop your vehicle to get reward.";
        }

        if (MenuManager.LevelNum2 == 2)
        {
            sentences.text = "This is the Traffic signal light. You Can only cross this when light is green. Stop if the light is red.";
        }

        if (MenuManager.LevelNum2 == 3)
        {
            sentences.text = "This is the yield signal. When you see this signal on the road you need to slow down your vehicle" +
                " to pass other vehicles.";
        }

        if (MenuManager.LevelNum2 == 4)
        {
            sentences.text = "This is the Speed Limt Sign. When you see this sign on road you need to adjust your speed occording to speed limit.";
        }

        if (MenuManager.LevelNum2 == 5)
        {
            sentences.text = "This is your test Level to know how much you remember the previous signals.";
        }

        if (MenuManager.LevelNum2 == 6)
        {
            sentences.text = "This is the Turn Right Signal When you see this signal on road you have to take right turn.";
        }

        if (MenuManager.LevelNum2 == 7)
        {
            sentences.text = "This is the Slippery Road Sign. When you see this signal on road you need to drive more carefully because" +
                " the road is slippery ahead.";
        }

        if (MenuManager.LevelNum2 == 8)
        {
            sentences.text = "This is the Turn Left Signal When you see this signal on road you have to take Left turn.";
        }

        if (MenuManager.LevelNum2 == 9)
        {
            sentences.text = "This is the Speed Limt Sign. When you see this sign on road you need to adjust your speed occording to speed limit.";
        }

        if (MenuManager.LevelNum2 == 10)
        {
            sentences.text = "This is the Road Bend Sign. This indicates that the road turns left then right so drive carefully.";
        }
        if (MenuManager.LevelNum2 == 11)
        {
            sentences.text = "This is the Speed Braker Sign. This indicates that the road has Speed Brakers ahead so Drive Carefully.";
        }
        if (MenuManager.LevelNum2 == 12)
        {
            sentences.text = "This is the Road Sign. This indicates that the road work is in progress ahead.";
        }
        if (MenuManager.LevelNum2 == 13)
        {
            sentences.text = "This is the Not Left Turn Sign. This indicates that you can not turn left on this road.";
        }
        if (MenuManager.LevelNum2 == 14)
        {
            sentences.text = "This is the No Parking Sign. This indicates that you can not Park at this place.";
        }
        if (MenuManager.LevelNum2 == 15)
        {
            sentences.text = "This is the Speed Limt Sign. When you see this sign on road you need to adjust your speed occording to speed limit.";
        }
        if (MenuManager.LevelNum2 == 16)
        {
            sentences.text = "This is the Not U Turn Sign. This indicates that you can not take U Turn at this place.";
        }
        if (MenuManager.LevelNum2 == 17)
        {
            sentences.text = "This is the Road Closed Sign. This indicates that the road closed ahead.";
        }
        if (MenuManager.LevelNum2 == 18)
        {
            sentences.text = "This is the Not Left Turn Sign. This indicates that you can not turn left on this road.";
        }
        if (MenuManager.LevelNum2 == 19)
        {
            sentences.text = "This is your test Level to know how much you remember the previous signals.";
        }
    }

    public void skipAnimation()
    {
        OnBtnClickSound();
        if (MenuManager.DrivingMode)
        {
            animCam2[MenuManager.LevelNum2].SetActive(false);
            RccCamera.enabled = true;
            skip.SetActive(false);
            signMessage.SetActive(true);
            //missionPanel.SetActive(false);
            //GameplayPanel.SetActive(true);
        }else
        {
            //print("skipp");
            RccCamera.enabled = true;
            skip.SetActive(false);
            GameplayPanel.SetActive(true);
        }



    }

    IEnumerator waitForBusTransform()
    {
        yield return new WaitForSeconds(1f);

        myPlayer.rigid.isKinematic = false;
    }


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

    public void HideRect()
    {
        if (PlayerPrefs.GetInt("removeads") == 0)
        {
            AdsManager.Instance.HideRectBannerAd();
        }
    }
    public void ShowRect()
    {
        if (PlayerPrefs.GetInt("removeads") == 0)
        {
            AdsManager.Instance.ShowRectBannerAd();
        }
    }


    public void WatchVideo()
    {
        //BtnClickSound();
        AdsManager.Instance.ShowRewardedVideoAd();
    }

}
