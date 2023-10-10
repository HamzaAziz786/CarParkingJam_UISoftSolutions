using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine;

public class AdmobController : MonoBehaviour
{
    public enum BannerAdType
    {
        BANNER,
        IAP_BANNER,
        LEADER_BOARD,
        MEDIUM_RECTANGE,
        SMART_BANNER,
        Adaptive,
    };

    #region Decleration

    [Header("Network Ids")]
    [SerializeField] private string appID;
    [SerializeField] private string bannerID;
    [SerializeField] private string interstitialID;
    [SerializeField] private string interstitialID_Floor;
    [SerializeField] private string rewardedID,AppOpenID;

    [Header("Banner Ads Settings")]
    [SerializeField] private BannerAdType banneryAdType = BannerAdType.Adaptive;
    [SerializeField] private AdPosition[] bannerAdPosition;
    private AdSize adSize = AdSize.Banner;

    public int selectedAdPosition_Banner = 0;

    private string testBannerID = "ca-app-pub-3940256099942544/6300978111";
    private string testInterstitialID = "ca-app-pub-3940256099942544/1033173712";
    private string testRewardedID = "ca-app-pub-3940256099942544/5224354917";

    private BannerView bannerViewAd;
    public BannerView LargBannerView;
    private InterstitialAd interstitialAd;
    private InterstitialAd interstitialAd_Floor;
    private RewardedAd rewardedAd;

  // [HideInInspector] public AppOpenAd AppOpen_Ad;
    private AdResult currentAdResult;
    public  delegate void Adloaded();
    public event Adloaded ShowLoaded;
    public GameObject AdBreak;
    #endregion
    private void Awake()
    {
        
        if(AdsController.instance.IsInternetConnection)
             Med_Initialize();
    }
    public void Med_Initialize()
    {
        MobileAds.Initialize(initStatus => {
            MobileAds.RaiseAdEventsOnUnityMainThread = true;
            // AppStateEventNotifier.AppStateChanged += OnAppStateChanged; //for focus
        });
        
    }
    private void OnAppStateChanged(AppState state)
    {
        if (state == AppState.Foreground)
        {
            HideBannerAd();
            //Invoke(nameof(ShowAd_AppOpen),0.5f);
        }
        if (state == AppState.Background)
        {
            HideBannerAd();
        }
    }
    private AdRequest CreateAdRequest()
    {
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");
        return adRequest;
    }
    #region Banner Ads
    public void RequestBannerAd()
    {
        if (AdsController.instance.CheckInitialization)
        {
            if (bannerViewAd != null)
            {
                bannerViewAd.Destroy();
            }

            if (bannerID == "" || bannerID == null || AdsController.instance.testMode)
            {
                bannerID = testBannerID;
            }
            AdSize adaptiveSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
            this.bannerViewAd = new BannerView(bannerID, adaptiveSize, bannerAdPosition[selectedAdPosition_Banner]);
            this.bannerViewAd.LoadAd(CreateAdRequest());
        }
        else
            selectedAdPosition_Banner = selectedAdPosition_Banner == 1 ? 0 : 1;
    }
    public bool ShowBannerAd(int bannerPositionIndex)
    {
        if (bannerViewAd == null || selectedAdPosition_Banner != bannerPositionIndex)
        {
            selectedAdPosition_Banner = bannerPositionIndex;
            RequestBannerAd();
            return false;
        }
            bannerViewAd.Show();
            return true;

    }
    public void HideBannerAd()
    {
      
            if (bannerViewAd != null)
            {
                bannerViewAd.Hide();
            }
    }

    public void RequestLargbanner()
    {
        if (AdsController.instance.CheckInitialization)
        {
            if (LargBannerView != null)
            {
                LargBannerView.Destroy();
            }
            if (bannerID == "" || bannerID == null || AdsController.instance.testMode)
            {
                bannerID = testBannerID;
            }
            LargBannerView = new BannerView(bannerID, AdSize.MediumRectangle, AdPosition.Bottom);
            LargBannerView.LoadAd(CreateAdRequest());
            
        }
        HideBannerAd();
    }
    public bool ShowLargeBannerAd()
    {
        if (LargBannerView == null)
        {
            RequestLargbanner();
            return false;
        }
            HideBannerAd();
            LargBannerView.Show();
            return true;
      
    }
    public void HideLargeBannerAd()
    {
      
            if (LargBannerView != null)
            {
                LargBannerView.Hide();
            }
 
    }
    #endregion
    #region Interstitial Ads
    public void RequestInterstitialAd_Floor()
    {
        try
        {
            if (AdsController.instance.CheckInitialization)
            {
                if (interstitialAd_Floor != null)
                {
                    if (interstitialAd_Floor.CanShowAd())
                        return;
                    else
                    {
                        interstitialAd_Floor.Destroy();
                        interstitialAd_Floor = null;
                    }
                }

                if (interstitialID_Floor == "" || interstitialID_Floor == null || AdsController.instance.testMode)
                {
                    interstitialID_Floor = testInterstitialID;
                }

                    InterstitialAd.Load(interstitialID_Floor, CreateAdRequest(),
                    (InterstitialAd ad, LoadAdError error) =>
                    {
                        // if error is not null, the load request failed.
                        if (error != null || ad == null)
                        {
                            Debug.LogError("interstitial ad failed to load an ad " +
                                           "with error : " + error);
                            interstitialAd_Floor = null;
                            return;
                        }
                        interstitialAd_Floor = ad;
                        RegisterReloadHandler(interstitialAd_Floor);
                    });
            }
        }
        catch
        {
            Debug.LogError("InterstitialAd Load Issue ");
        }
    }
    public bool IsInterstitialAdLoaded_Floor()
    {
        if (interstitialAd_Floor == null)
        {
            RequestInterstitialAd_Floor();
            return false;
        }
        if(interstitialAd_Floor.CanShowAd())
           return true;
        return false;

    }
    public bool ShowInterstitialAd_Floor()
    {

        if (AdsController.instance.CheckInitialization)
        {
            if (IsInterstitialAdLoaded_Floor())
            {
                interstitialAd_Floor.Show();
                return true;
            }
            else
            {
                AdsController.instance.ShowUnityVideoAd();
                return false;
            }
            
        }
            AdsController.instance.RewardedAdResult(AdResult.SUCCSSEFUL);
            return false;
    }
    public void RequestInterstitialAd()
    {
        if (AdsController.instance.CheckInitialization)
        {
            if (interstitialAd != null)
            {
                if (interstitialAd.CanShowAd())
                    return;
                else
                {
                    interstitialAd.Destroy();
                    interstitialAd = null;
                }
            }
            if (interstitialID == "" || interstitialID == null || AdsController.instance.testMode)
            {
                interstitialID = testInterstitialID;
            }

                InterstitialAd.Load(interstitialID, CreateAdRequest(),
              (InterstitialAd ad, LoadAdError error) =>
              {
                  // if error is not null, the load request failed.
                  if (error != null || ad == null)
                  {
                      Debug.LogError("interstitial ad failed to load an ad " +
                                     "with error : " + error);
                      interstitialAd = null;
                      return;
                  }
                  interstitialAd = ad;
                  RegisterReloadHandler_Inter(interstitialAd);
              });
        }
    }
    public bool IsInterstitialAdLoaded()
    {
        if (interstitialAd == null )
        {
            RequestInterstitialAd();
            return false;
        }
        if(interstitialAd.CanShowAd())
            return true;
         return false;
    }
    public bool ShowInterstitialAd()
    {
        if (AdsController.instance.CheckInitialization)
        {
            if (IsInterstitialAdLoaded())
            {
                interstitialAd.Show();
                return true;
            }
           
        }
        AdsController.instance.RewardedAdResult(AdResult.SUCCSSEFUL);
        return false;
    }
    #endregion
    #region Rewarded Ads
    public void RequestRewardedAd()
    {
        try
        {
            if (AdsController.instance.CheckInitialization)
            {
                if (rewardedAd != null)
                {
                    if (rewardedAd.CanShowAd())
                        return;
                    else
                    {
                        rewardedAd.Destroy();
                        rewardedAd = null;
                    }
                }
                if (rewardedID == "" || rewardedID == null || AdsController.instance.testMode)
                {
                    rewardedID = testRewardedID;
                }
                    RewardedAd.Load(rewardedID, CreateAdRequest(),
                    (RewardedAd ad, LoadAdError error) =>
                    {
                        // if error is not null, the load request failed.
                        if (error != null || ad == null)
                        {
                            Debug.LogError("Rewarded ad failed to load an ad " +
                                           "with error : " + error);
                            rewardedAd = null;
                            return;
                        }
                        rewardedAd = ad;
                        RegisterReloadHandler(rewardedAd);
                    });
            }
        }
        catch
        {
            Debug.LogError("Rewarded Ad Load Issue ");
        }
    }
    public bool IsRewardedAdLoaded()
    {
        if (rewardedAd == null)
        {
            RequestRewardedAd();
            return false;
        }
        if(rewardedAd.CanShowAd())
           return true;
       return false;
    }
    public bool ShowRewardedAd()
    {

        if (AdsController.instance.CheckInitialization)
        {
            if (IsRewardedAdLoaded())
            {
                rewardedAd.Show((Reward reward) =>
                {
                });
                return true;
            }
        }
        return false;
    }
    #endregion
    //public void LoadAd_AppOpen()
    //{
    //    if (AdsController.instance.CheckInitialization)
    //    {
    //        //if (AdsController.instance.testMode)
    //        //{
    //        //    AppOpenID = "ca-app-pub-4717190550790598/8109158374";
    //        //}
    //        //if (AppOpen_Ad != null)
    //        //{
    //        //    AppOpen_Ad.Destroy();
    //        //    AppOpen_Ad = null;
    //        //}
    //        // Create our request used to load the ad.
    //        var adRequest = new AdRequest();
    //        // send the request to load the ad.
    //        //AppOpenAd.Load(AppOpenID, ScreenOrientation.Portrait, adRequest,
    //        // (AppOpenAd ad, LoadAdError error) =>
    //        // {
    //        //     // if error is not null, the load request failed.
    //        //     if (error != null || ad == null)
    //        //     {
    //        //         Debug.LogError("app open ad failed to load an ad " +
    //        //                        "with error : " + error);
    //        //         return;
    //        //     }
    //        //     AppOpen_Ad = ad;
    //        //     RegisterEventHandlers(ad);
    //        //     if (ShowLoaded != null)
    //        //     {
    //        //         ShowLoaded();
    //        //     }
    //        // });
    //    }
    //}
    GameObject Ad_Panel;
    //public void ShowAd_AppOpen()
    //{
    //    try
    //    {
    //        if (AdsController.instance.CheckInitialization)
    //        {
    //            if (AppOpen_Ad == null)
    //            {
    //                Invoke(nameof(LoadAd_AppOpen), 1f);
    //                return;
    //            }
    //            if (AppOpen_Ad.CanShowAd())
    //            {
    //                if (Ad_Panel == null)
    //                    Ad_Panel = Instantiate(AdBreak);
    //                AppOpen_Ad.Show();
    //            }
    //        }
    //    }
    //    catch
    //    {
    //        Debug.LogError("App open On Show Issue");
    //    }



    //}
    private void RegisterReloadHandler(InterstitialAd ad)
    {
        try
        {
        ad.OnAdFullScreenContentClosed += () =>
        {
            AdsController.instance.RewardedAdResult(AdResult.SUCCSSEFUL);
            RequestInterstitialAd_Floor();
        };
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            AdsController.instance.RewardedAdResult(AdResult.SUCCSSEFUL);
        };
        }
        catch 
        {

            Debug.LogError("FloorInter HANDLER iSSUE");
        }
    }
    private void RegisterReloadHandler_Inter(InterstitialAd ad)
    {
        try
        {
        ad.OnAdFullScreenContentClosed += () =>
        {
            AdsController.instance.RewardedAdResult(AdResult.SUCCSSEFUL);
            RequestInterstitialAd();
        };
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            AdsController.instance.RewardedAdResult(AdResult.SUCCSSEFUL);
        };
        }
        catch
        {

            Debug.LogError("Inter HANDLER iSSUE");
        }
    }
    private void RegisterReloadHandler(RewardedAd ad)
    {
        try
        {
        ad.OnAdFullScreenContentClosed += () =>
        {
            AdsController.instance.RewardedAdResult(AdResult.SUCCSSEFUL);
            RequestRewardedAd();

        };
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            AdsController.instance.RewardedAdResult(AdResult.FAILD);
        };
        }
        catch
        {

            Debug.LogError("Rewarded HANDLER iSSUE");
        }
    }
    private void RegisterEventHandlers(AppOpenAd ad)
    {
        try
        {
        ad.OnAdFullScreenContentClosed += () =>
        {
            if (Ad_Panel != null)
                Destroy(Ad_Panel);
            //AppOpen_Ad = null;
            //LoadAd_AppOpen();
            ShowBannerAd(selectedAdPosition_Banner);
        };
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            if (Ad_Panel != null)
                Destroy(Ad_Panel);
            //AppOpen_Ad = null;
        };
        }
        catch
        {

            Debug.LogError("AppOpen HANDLER iSSUE");
        }
    }

    #region Memory Information

    //readonly int memoryThreshHold = 300; //in MBs
    //static int memoryAvailable = 0;
    //static System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex(@"\d+");

    // return True if memory low by defined threshHold
    //public bool IsMemory(int threshold = -1)
    //{
    //    try
    //    {
    //        threshold = threshold.Equals(-1) ? memoryThreshHold : threshold;
    //        return LoadMemoryInfo().Equals(true) ? (memoryAvailable / 1024) > threshold : false;
    //    }
    //    catch { return true; }
    //}
    //static bool LoadMemoryInfo()
    //{
    //    try
    //    {
    //        //if file not exist retrun from here
    //        if (!System.IO.File.Exists("/proc/meminfo")) return false;
    //        System.IO.FileStream fs = new System.IO.FileStream("/proc/meminfo", System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
    //        System.IO.StreamReader sr = new System.IO.StreamReader(fs);
    //        string line;
    //        while ((line = sr.ReadLine()) != null)
    //        {
    //            line = line.ToLower().Replace(" ", "");
    //            if (line.Contains("memavailable")) { memoryAvailable = int.Parse(re.Match(line).Value); }
    //        }
    //        sr.Close(); fs.Close(); fs.Dispose();
    //        return true;
    //    }
    //    catch
    //    { return false; }
    //}

    #endregion
}
