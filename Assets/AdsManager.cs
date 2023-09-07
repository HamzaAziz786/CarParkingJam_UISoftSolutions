using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using Firebase;
using Firebase.Analytics;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using UnityEngine.Networking;

[Serializable]

public class AdsManager : MonoBehaviour {

    DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;//@@@@testing
    public static AdsManager Instance;
    [Space]
    [Space]
    [Tooltip("No mediation test suite")]
    [Header("AdMob Mediation V1.7")]

    public string AdmobAppID;
    public AdPosition BannerAdPosition ;
    public string BannerAdID;
    public string InterstitialMediationID;
    public string InterstitialLoadingID;
    public string RewardedVideoAdID;
    public string RectBannerAdID;
    [Space]
    public bool IsShowTestAds;
    public bool ShowDebugLogs;
    bool isVideoCompleted=false;
    [HideInInspector] public bool isRectBannerAdLoaded = false;
    [HideInInspector] public bool isRewardedVideoAdLoaded = false;

    private BannerView myBannerView,BannerAdViewBottom;
    private InterstitialAd myInterstitialMediationAd;
    private InterstitialAd myInterstitialLoadingAd;

    private RewardedAd rewardedAd;
    private BannerView myRectBannerView;
    private FirebaseApp app;

    [Space]
    [Header("Keys")]

    [Tooltip("Either use this \"removeads\" key or put your own here. Dont forget to set the value of that key 1 on succesful purchase of remove Ads")]
    public string removeAds_InAPPKey = "removeads";
  
   
    void Awake()
	{

        /// <summary>
        /// Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
            if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        //MobileAds.Initialize(initStatus => { });

        MobileAds.Initialize((initStatus) =>
        {
            Dictionary<string, AdapterStatus> map = initStatus.getAdapterStatusMap();
            foreach (KeyValuePair<string, AdapterStatus> keyValuePair in map)
            {
                string className = keyValuePair.Key;
                AdapterStatus status = keyValuePair.Value;
                switch (status.InitializationState)
                {
                    case AdapterState.NotReady:
                        // The adapter initialization did not complete.
                        MonoBehaviour.print("Adapter: " + className + " not ready.");
                        break;
                    case AdapterState.Ready:
                        // The adapter was successfully initialized.
                        MonoBehaviour.print("Adapter: " + className + " is initialized.");
                        break;
                }
            }
        });



        if (IsShowTestAds)
        {
            AdmobAppID = "ca-app-pub-3940256099942544~3347511713";////ca-app-pub-5198289504424797~2390211424
            BannerAdID = "ca-app-pub-3940256099942544/6300978111";////ca-app-pub-5198289504424797/3819623410
            RectBannerAdID = "ca-app-pub-3940256099942544/6300978111";
            InterstitialMediationID = "ca-app-pub-3940256099942544/1033173712";////ca-app-pub-5198289504424797/7758868427
            InterstitialLoadingID = "ca-app-pub-3940256099942544/1033173712";
            RewardedVideoAdID = "ca-app-pub-3940256099942544/5224354917";////ca-app-pub-5198289504424797/2071481379
        }
        
    }

    

   

    public void Start()
    {
        try
        {
            FireBaseInitilization();
        }
        catch (Exception ex)
        {
            if (ShowDebugLogs)
            {
                Debug.Log("Exception Generated" + ex);
            }
        }
        this.rewardedAd = new RewardedAd(RewardedVideoAdID);

        InItrewardVideoEvents();

        RequestBannerAd();
        //RequestInterstitialMediation();
        RequestInterstitialLoading();
        RequestRewardedVideoAd();
        //RequestRectBannerAd();



    }
   
    //***************************** Banner Ad *****************************************************************************
    #region Banner Ads 
    // Admob banner Ads fuction
    public void RequestBannerAd()
    {
		
            if (myBannerView != null)
                myBannerView.Destroy();
            this.myBannerView = new BannerView (BannerAdID, AdSize.Banner, BannerAdPosition);
            InItBannerEvents();
		    AdRequest request = new AdRequest.Builder ().Build ();
		    this.myBannerView.LoadAd (request);
			HideBannerAd ();
	
	}
    public void ShowBannerAd()
    {
        if (PlayerPrefs.GetInt(removeAds_InAPPKey)==0)
        {
            // RequestBannerAd();
            if (myBannerView != null)
            {
                myBannerView.Show();
            }
            else {
                RequestBannerAd();
            }

        }
    }
    public void HideBannerAd()
    {
        if (myBannerView != null)
        {
            myBannerView.Hide();
        }
    }
    public void RequestBannerReposition(AdPosition position)
    {
		
            if (myBannerView != null)
                myBannerView.Destroy();
            //this.BannerAdViewBottom = new BannerView (BannerAdID, AdSize.Banner, AdPosition.Bottom);
            this.myBannerView = new BannerView(BannerAdID, AdSize.Banner, position);
            InItBannerEvents();
		    AdRequest request2 = new AdRequest.Builder ().Build ();
		    this.myBannerView.LoadAd (request2);
			//HideBannerAd ();
	    
	}
	
    public void ShowBannerReposition(AdPosition position)
    {
        if (PlayerPrefs.GetInt(removeAds_InAPPKey)==0)
        {
            RequestBannerReposition(position);
        }
    }
	
    public void HideBannerAdBottom()
    {
        if (myBannerView != null)
        {
            myBannerView.Hide();
        }
    }
    #endregion


    //*****************************Rect Banner Ad *****************************************************************************
    #region Banner Ads 
    // Admob banner Ads fuction
    public void RequestRectBannerAd()
    {
        
        

            AdSize adSizemiddleleft = new AdSize(240, 200);

            //middle left           
          //  int placeY = (int)((Screen.width / 2) * 0.055);
          //  int placeX = 0;
        //    this.myRectBannerView = new BannerView(RectBannerAdID, adSizemiddleleft, placeX, placeY);
        this.myRectBannerView = new BannerView(RectBannerAdID, adSizemiddleleft, AdPosition.BottomLeft);

            //middle Right
            //int placeY = (int)((Screen.width / 2) * 0.055);
            //int placeX = (int)(Screen.width - 300);
            //this.myRectBannerView = new BannerView(RectBannerAdID, adSizemiddleleft, placeX, placeY);


            InItRectBannerEvents();
            AdRequest request = new AdRequest.Builder().Build();
            this.myRectBannerView.LoadAd(request);
            HideRectBannerAd();
        
    }

    public void ShowRectBannerAd()
    {
        
        if (PlayerPrefs.GetInt(removeAds_InAPPKey, 0) == 0)
        {
            if (isRectBannerAdLoaded)
            {
                myRectBannerView.Show();
            }

        }
    }
    public void HideRectBannerAd()
    {
        if(myRectBannerView!=null){
        myRectBannerView.Hide();
        }
    }

    #endregion

    //***************************** Interstitial Ad *****************************************************************************
    #region InterstitialAd
 //   public void RequestInterstitialMediation()
 //   {
		
 //           // Initialize an InterstitialAd.
 //           this.myInterstitialMediationAd = new InterstitialAd(InterstitialMediationID);
 //           InitInterstitialMediationEvents();
 //           // Create an empty ad request.
 //           AdRequest request = new AdRequest.Builder().Build();
 //           // Load the interstitial with the request.
 //           this.myInterstitialMediationAd.LoadAd(request);
 //           if (ShowDebugLogs)
 //               Debug.Log("Requested Interstitial Mediation");
        
	//}

	//public void ShowInterstitialMediation()
 //   {
 //       if (PlayerPrefs.GetInt(removeAds_InAPPKey)==0)
 //       {
 //           if (myInterstitialMediationAd.IsLoaded())
 //           {
 //               myInterstitialMediationAd.Show();
 //               //LogEvent("interstitial_showed");

 //           }
 //           else
 //           {
 //               RequestInterstitialMediation();
 //           }

 //       }
	//}


    public void RequestInterstitialLoading()
    {
       
            // Initialize an InterstitialAd.
            this.myInterstitialLoadingAd = new InterstitialAd(InterstitialLoadingID);
            InitInterstitialLoadingEvents();
            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the interstitial with the request.
            this.myInterstitialLoadingAd.LoadAd(request);
            if (ShowDebugLogs)
                Debug.Log("Requested Interstitial Loading");
        
    }

    public void ShowInterstitialLoading()
    {
        if (PlayerPrefs.GetInt(removeAds_InAPPKey)==0)
        {
            if (myInterstitialLoadingAd.IsLoaded())
            {
                myInterstitialLoadingAd.Show();
                //LogEvent("interstitial_showed");

            }
            else
            {
                RequestInterstitialLoading();
            }
        }
    }


     
    #endregion


    //***************************** RewardedVideo Ad *****************************************************************************
    #region RewardedVideoAd


    public void RequestRewardedVideoAd()
    {
        isRewardedVideoAdLoaded = false;


        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
        if (ShowDebugLogs)
                Debug.Log("Requested Rewarded Video Ad");
        
    }


    public void ShowRewardedVideoAd()
    {
        isVideoCompleted = false;
            if (isRewardedVideoAdLoaded)
            {
            rewardedAd.Show();
                //LogEvent("rewarded_showed");
            }
            else {
                RequestRewardedVideoAd();
            }
       
    }

    public void OnRewardedVideoComplete()
    {
        //give your Rewards Here

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            if (MenuManager.instance.MainPanel.activeSelf || MenuManager.instance.ShopPanel.activeSelf)
            {
                PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") + 1000);
            }
            else if (MenuManager.instance.LevelPanel.activeSelf && !MenuManager.instance.ShopPanel.activeSelf)
            {
                if (PlayerPrefs.GetInt("levels") < 29)
                {
                    PlayerPrefs.SetInt("levels", PlayerPrefs.GetInt("levels") + 1);
                }

                LevelsPanel.instance.SelectLevel();

            }
            else if (MenuManager.instance.StorePanel.activeSelf && !MenuManager.instance.ShopPanel.activeSelf)
            {
                Store_Script.instance.buyBtn.SetActive(false);
                PlayerPrefs.SetInt("car" + Store_Script.instance.num, 1);
                Store_Script.instance.startBtn.SetActive(true);
                PlayerPrefs.SetInt("currentPlayer", Store_Script.instance.num);
                Store_Script.instance.WatchVideoBtn.SetActive(false);
            }
        }
        else if (SceneManager.GetActiveScene().name == "GamePlay")
        {
            if (GamePlay_Manager.Instance.FailPanel.activeSelf)
            {
                if (PlayerPrefs.GetInt("levels") < 29)
                {
                    PlayerPrefs.SetInt("levels", PlayerPrefs.GetInt("levels") + 1);
                }
                GamePlay_Manager.Instance.OnNext();
            }
            else if (GamePlay_Manager.Instance.CompletePanel.activeSelf)
            {
                GamePlay_Manager.Instance.OnRewardDouble();
            }
        }

        //GameManager.IsVideoComplete = true;
    }

    public void OnRewardedVideoSkipped()
    {
       // ShowToast.showToastMessage("On Rewarded Video Skipped");
        if (ShowDebugLogs)
            Debug.Log("On Rewarded Video Skipped");
    }

    #endregion

    //***************************** Ads Events *****************************************************************************
    #region Events

    #region Banner Events
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
       if(ShowDebugLogs)
        Debug.Log("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {

        if (ShowDebugLogs)
            MonoBehaviour.print("HandleFailedToReceiveAd event received with message: ");

    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        if (ShowDebugLogs)
            Debug.Log("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        if (ShowDebugLogs)
            Debug.Log("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        if (ShowDebugLogs)
            Debug.Log("HandleAdLeavingApplication event received");
    }

    #endregion


    #region Interstatial Events
    //InterstitiaL Mediation EVENTS

    public void HandleOnInterstatialMediationAdLoaded(object sender, EventArgs args)
    {
        if (ShowDebugLogs)
            Debug.Log("HandleAdLoaded event received");
            
    }

    public IEnumerator callRelevantRequest(string request)
    {
        yield return new WaitForSeconds(5.0f);
        CancelInvoke(request);
        Invoke(request, 0);
        if (ShowDebugLogs)
        {
            Debug.Log("calling "+ request);
        }
    }

    public void HandleOnInterstatialMediationAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
       
        if (ShowDebugLogs)
            Debug.Log("HandleFailedToReceiveAd Mediated Interstitial event received with message: "
                            );
    }

    public void HandleOnInterstatialMediationAdOpened(object sender, EventArgs args)
    {
        if (ShowDebugLogs){
            Debug.Log("HandleAdOpened event received");
        }
        //LogEvent("interstitial_clicked");
            
    }

    public void HandleOnInterstatialMediationAdClosed(object sender, EventArgs args)
    {
        //RequestInterstitialMediation();

        if (ShowDebugLogs)
            Debug.Log("HandleAdClosed event received");
    }

    public void HandleOnInterstatialMediationAdLeavingApplication(object sender, EventArgs args)
    {
        if (ShowDebugLogs)
            Debug.Log("HandleAdLeavingApplication event received");
    }

    //InterstitiaL Loading EVENTS
    public void HandleOnInterstatialLoadingAdLoaded(object sender, EventArgs args)
    {
        if (ShowDebugLogs)
            Debug.Log("HandleAdLoaded event received");

    }

    public void HandleOnInterstatialLoadingAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        //CancelInvoke("RequestInterstitialLoading");

        //Invoke("RequestInterstitialLoading", 5.0f);
       

        if (ShowDebugLogs)
            Debug.Log("HandleFailedToReceiveAd Loading Interstitial event received with message: "
                            );
    }

    public void HandleOnInterstatialLoadingAdOpened(object sender, EventArgs args)
    {
        if (ShowDebugLogs)
        {
            Debug.Log("HandleAdOpened event received");
        }
        //LogEvent("interstitial_clicked");

    }

    public void HandleOnInterstatialLoadingAdClosed(object sender, EventArgs args)
    {
        RequestInterstitialLoading();
        if (ShowDebugLogs)
            Debug.Log("HandleAdClosed event received");
    }

    public void HandleOnInterstatialLoadingAdLeavingApplication(object sender, EventArgs args)
    {
        if (ShowDebugLogs)
            Debug.Log("HandleAdLeavingApplication event received");
    }

   

    #endregion


    #region Rewarded Video Events
    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        isRewardedVideoAdLoaded = true;
        if (ShowDebugLogs)
            Debug.Log("HandleRewardBasedVideoLoaded event received");
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        isRewardedVideoAdLoaded = false;
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        if (ShowDebugLogs){
            Debug.Log("HandleRewardBasedVideoOpened event received");
        }
        //LogEvent("rewarded_clicked");
           
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        if (ShowDebugLogs)
            Debug.Log("HandleRewardBasedVideoStarted event received");

    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        isRewardedVideoAdLoaded = false;
        RequestRewardedVideoAd();
        if (!isVideoCompleted)
        {
            OnRewardedVideoSkipped();
        }
        if (ShowDebugLogs)
            Debug.Log("HandleRewardBasedVideoClosed event received");
        
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        isVideoCompleted = true;
        OnRewardedVideoComplete();
        
        if (ShowDebugLogs)
            Debug.Log(
            "HandleRewardBasedVideoRewarded event received for "
                        + amount.ToString() + " " + type);
        
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        if (ShowDebugLogs)
            Debug.Log("HandleRewardBasedVideoLeftApplication event received");
    }
    #endregion


    #region RectBanner Events
    public void HandleOnRectBannerAdLoaded(object sender, EventArgs args)
    {
        isRectBannerAdLoaded = true;
        if(ShowDebugLogs)
        Debug.Log("HandleAdLoaded event received");
    }

    public void HandleOnRectBannerAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        isRectBannerAdLoaded = false;
        //RequestRectBannerAd();
        //CancelInvoke("RequestRectBannerAd");
        //Invoke("RequestRectBannerAd", 5.0f); //_FloatValues[0].value
       

        if (ShowDebugLogs)
            Debug.Log("HandleFailedToReceiveAd Rect Banner event received with message: "
                            );
    }

    public void HandleOnRectBannerAdOpened(object sender, EventArgs args)
    {
        if (ShowDebugLogs)
            Debug.Log("HandleAdOpened event received");
    }

    public void HandleOnRectBannerAdClosed(object sender, EventArgs args)
    {
        if (ShowDebugLogs)
            Debug.Log("HandleAdClosed event received");
    }

    public void HandleOnRectBannerAdLeavingApplication(object sender, EventArgs args)
    {
        if (ShowDebugLogs)
            Debug.Log("HandleAdLeavingApplication event received");
    }

    #endregion


    void InItBannerEvents()
    {
        // Called when an ad request has successfully loaded.
        this.myBannerView.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.myBannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        this.myBannerView.OnAdOpening += HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        this.myBannerView.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
       // this.myBannerView.OnAdLeavingApplication += HandleOnAdLeavingApplication;

    }

    void InitInterstitialMediationEvents()

    { 
        // Called when an ad request has successfully loaded.
        this.myInterstitialMediationAd.OnAdLoaded += HandleOnInterstatialMediationAdLoaded;
        // Called when an ad request failed to load.
        this.myInterstitialMediationAd.OnAdFailedToLoad += HandleOnInterstatialMediationAdFailedToLoad;
        // Called when an ad is shown.
        this.myInterstitialMediationAd.OnAdOpening += HandleOnInterstatialMediationAdOpened;
        // Called when the ad is closed.
        this.myInterstitialMediationAd.OnAdClosed += HandleOnInterstatialMediationAdClosed;
        // Called when the ad click caused the user to leave the application.
      //  this.myInterstitialMediationAd.OnAdLeavingApplication += HandleOnInterstatialMediationAdLeavingApplication;

    }

    void InitInterstitialLoadingEvents()

    {
        // Called when an ad request has successfully loaded.
        this.myInterstitialLoadingAd.OnAdLoaded += HandleOnInterstatialLoadingAdLoaded;
        // Called when an ad request failed to load.
        this.myInterstitialLoadingAd.OnAdFailedToLoad += HandleOnInterstatialLoadingAdFailedToLoad;
        // Called when an ad is shown.
        this.myInterstitialLoadingAd.OnAdOpening += HandleOnInterstatialLoadingAdOpened;
        // Called when the ad is closed.
        this.myInterstitialLoadingAd.OnAdClosed += HandleOnInterstatialLoadingAdClosed;
        // Called when the ad click caused the user to leave the application.
       // this.myInterstitialLoadingAd.OnAdLeavingApplication += HandleOnInterstatialLoadingAdLeavingApplication;

    }


    void InItrewardVideoEvents()
    { 
        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardBasedVideoLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardBasedVideoOpened;

        // Called when the user should be rewarded for watching a video.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardBasedVideoClosed;
   

    }


    void InItRectBannerEvents()
    {
        // Called when an ad request has successfully loaded.
        this.myRectBannerView.OnAdLoaded += HandleOnRectBannerAdLoaded;
        // Called when an ad request failed to load.
        this.myRectBannerView.OnAdFailedToLoad += HandleOnRectBannerAdFailedToLoad;
        // Called when an ad is clicked.
        this.myRectBannerView.OnAdOpening += HandleOnRectBannerAdOpened;
        // Called when the user returned from the app after an ad click.
        this.myRectBannerView.OnAdClosed += HandleOnRectBannerAdClosed;
        // Called when the ad click caused the user to leave the application.
       // this.myRectBannerView.OnAdLeavingApplication += HandleOnRectBannerAdLeavingApplication;

    }

    #endregion

    private void FireBaseInitilization()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                app = Firebase.FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });

        //FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        //{
        //    dependencyStatus = task.Result;
        //    if (dependencyStatus == DependencyStatus.Available)
        //    {
        //        Dictionary<string, object> defaults = new Dictionary<string, object>();

        //        if (ShowDebugLogs)
        //        {
        //            Debug.Log("Enabling data collection.");
        //        }
        //        FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);

        //        if (ShowDebugLogs)
        //        {
        //            Debug.Log("Set user properties.");
        //        }
        //        // Set the user's sign up method.
        //        FirebaseAnalytics.SetUserProperty(FirebaseAnalytics.UserPropertySignUpMethod, "Google");
        //        // Set the user ID.
        //        FirebaseAnalytics.SetUserId("uber_user_510");
        //        // Set default session duration values.
        //        FirebaseAnalytics.SetMinimumSessionDuration(new TimeSpan(0, 0, 10));
        //        FirebaseAnalytics.SetSessionTimeoutDuration(new TimeSpan(0, 30, 0));

        //        // These are the values that are used if we haven't fetched data from the
        //        if (!_isArrayNullOrEmpty(_FloatValues))
        //        {
        //            foreach (var item in _FloatValues)
        //            {
        //                defaults.Add(item.Name, item.defaultValue);

        //            }
        //        }

        //        if (!_isArrayNullOrEmpty(_StringValues))
        //        {
        //            foreach (var item in _StringValues)
        //            {
        //                defaults.Add(item.Name, item.defaultValue);
        //            }
        //        }

        //        if (!_isArrayNullOrEmpty(_BoleanValues))
        //        {
        //            foreach (var item in _BoleanValues)
        //            {
        //                defaults.Add(item.Name, item.defaultValue);
        //            }
        //        }



        //        Firebase.RemoteConfig.FirebaseRemoteConfig.SetDefaults(defaults);
        //        if (ShowDebugLogs)
        //        {
        //            Debug.Log("RemoteConfig configured and ready!");
        //        }
        //        FetchDataAsync();


        //    }
        //    else
        //    {
        //        if (ShowDebugLogs)
        //            Debug.LogError(

        //                "Could not resolve all Firebase dependencies: " + dependencyStatus);
        //    }
        //});
    }




    /////// <summary>
    ///////trigger firebase(Analytics) custom log Event with single parameter
    public void LogEvent(string name)
    {
        try
        {

            FirebaseAnalytics.LogEvent(name);
            if (ShowDebugLogs)
                Debug.Log("Logging an Event: " + name);
        }
        catch (Exception ex)
        {
            if (ShowDebugLogs)
            {
                Debug.Log("Exception Generated" + ex);

            }
        }

    }


}

