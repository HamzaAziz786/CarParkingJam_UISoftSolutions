using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public enum AdNetwork
{
    UNITY,
    ADMOB,
};

public enum AdType
{
    BANNER,
    INTERSTITIAL,
    REWARDED,
    LargeBanner
};

public enum AdResult
{
    SUCCSSEFUL,
    FAILD,
};
[RequireComponent(typeof(AdmobController))]
public class AdsController : MonoBehaviour
{
    #region Declaration

    public static AdsController instance;
    public delegate void OnAdSuccess();
    public OnAdSuccess onAdSuccess;
    public bool testMode = false;
    [Space(15)]
    public AdmobController admobController;
    [Space(15)]
    private bool isProUser = false;
    private bool isInternet = false;
    public bool isAdInitialized = false;
    //Unity ADs
    [Space(15)]
    [Header("Unity Ids")]
    [SerializeField]
    public string unityAdsID;
    [SerializeField] private string interstitialPlacementID;
    [SerializeField] private string URV_PlacementID;
    [HideInInspector] public int CountP = 0, CountM = 1, CountInter = 0;
    public delegate void IAPSuccess();
    public IAPSuccess FunRetCal;
    public GameObject LoadingPanel;
    #endregion
    #region Unity Functions And Loading Sequence
    void Awake()
    {
        if (instance != null) Destroy(this.gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        if (!PlayerPrefs.HasKey("newUser"))
        {
            PlayerPrefs.SetInt("newUser", 0);
            PlayerPrefs.GetInt("GameLoad", 0);
        }
        CountM = 1;
       // Application.targetFrameRate = 60;
    }

    IEnumerator Start()
    {
        
        if (PlayerPrefs.GetInt("remove_ads") == 1)
            isProUser = true;
        else
            isProUser = false;

            //admobController.ShowLoaded += loadSceen;
        if (IsInternetConnection)
        {
            DoAdsInitialization();
            yield return new WaitForSeconds(2f);
            if (!isProUser)
            {
               // admobController.LoadAd_AppOpen();
                admobController.RequestBannerAd();
                admobController.RequestInterstitialAd_Floor();
                //if (PlayerPrefs.GetInt("GameLoad") == 0)
                //{
                //    loadSceen();
                //    PlayerPrefs.SetInt("GameLoad", 1);
                //}
                //else
                //{
                //    Invoke(nameof(loadSceen), 15f);
                //    //Invoke(nameof(AppopenLoad), 9f);
                //    PlayerPrefs.SetInt("GameLoad", 0);
                //}
            }
            admobController.RequestRewardedAd();
        }
        else
        {
            isAdInitialized = false;
          yield return new WaitForSeconds(1f);
            //loadSceen();
        }
    }
    //void AppopenLoad()
    //{
    //    if(admobController.AppOpen_Ad == null)
    //        admobController.LoadAd_AppOpen();
    //}
  public  bool onceAppOpen = false;
  //public void loadSceen()
  //{
  //      if (!onceAppOpen)
  //      {
  //          StartCoroutine(WaitLoding());
  //          onceAppOpen = true;
  //      }
  //}
  //  IEnumerator WaitLoding()
  //  {
  //      //if (PlayerPrefs.GetInt("GameLoad") != 1)
  //      //    admobController.ShowAd_AppOpen();
  //      yield return new WaitForSeconds(3f);
  //        SceneManager.LoadScene("Menu");
  //  }

   
    public bool IsInternetConnection
    {
        get
        {
            isInternet = Application.internetReachability != NetworkReachability.NotReachable;
            return isInternet;
        }
    }
    public bool CheckInitialization
    {
        get
        {
            if (IsInternetConnection)
            {
                if (isAdInitialized)
                {
                    return true;
                }
                else
                {

                    admobController.Med_Initialize();
                    DoAdsInitialization();
                    return false;
                }
            }else
                return false;
        }
    }
  
    #endregion
    //---------------------------------------------- Unity Functions And Loading Sequence -----------------------------------------------------------------//
     public bool UnityAdLoaded = false , UnityInter = false;
    public void DoAdsInitialization()
    {
        try
        {
            isAdInitialized = true;
          
        }
        catch { Debug.LogError("Admob Cannot Initialize"); }
    }
    public void LoadAd(string _adUnitId)
    {
       
    }
    public bool ShowUnityVideoAd()
    {
        try
        {
            if (UnityInter)
            {
                RewardedAdResult(AdResult.SUCCSSEFUL);
               
            }
            else
                RewardedAdResult(AdResult.SUCCSSEFUL);
            return true;
        }
        catch { return false; }
    }
    public bool ShowUnityRewarded()
    {
        
        return true;
    }
    //---------------------------------------------- Showing Ad -------------------------------------------------------------------------------------------//
    #region Showing Ad

    public bool ShowAd(AdType adType, int bannerPositionIndex = 0)
    {
            return AdmobShowAd(adType, bannerPositionIndex);
    }
    public bool ShowAd(AdNetwork adNetwork, AdType adType)
    {
                switch (adNetwork)
                {
                    case AdNetwork.ADMOB:
                        return AdmobShowAd(adType);
                    case AdNetwork.UNITY:
                       // return AdmobShowAd(adType);
                        return UnityShowAd(adType);
                    default:
                        return false;
                }
    }
    public bool ShowAd(AdNetwork adNetwork, AdType adType, OnAdSuccess successCallBack)
    {
        if (CheckInitialization)
        {

            onAdSuccess = null;
            onAdSuccess += successCallBack;

            switch (adNetwork)
            {
                case AdNetwork.ADMOB:
                    return AdmobShowAd(adType);
                case AdNetwork.UNITY:
                   // return AdmobShowAd(adType);
                    return UnityShowAd(adType);
                default:
                    return false;
            }
        }
        else {
            onAdSuccess = null;
            onAdSuccess += successCallBack;
            RewardedAdResult(AdResult.SUCCSSEFUL);
            return false;
        }
    }

    public void HideBanner()
    {
        admobController.HideBannerAd();
    }
  
    private bool AdmobShowAd(AdType adType, int bannerPositionIndex = 0)
    {
        switch (adType)
        {
            case AdType.BANNER:
                if (isProUser)
                    return false;

                return admobController.ShowBannerAd(bannerPositionIndex);

            case AdType.INTERSTITIAL:
                if (isProUser)
                    return false;

                return admobController.ShowInterstitialAd_Floor();

            case AdType.REWARDED:
                return admobController.ShowRewardedAd();
            case AdType.LargeBanner:
                if (isProUser)
                    return false;
                return admobController.ShowLargeBannerAd();
            default:
                return false;
        }
    }
    private bool UnityShowAd(AdType adType)
    {
        switch (adType)
        {
            case AdType.INTERSTITIAL:
                if (isProUser)
                    return false;

                return ShowUnityVideoAd();

            case AdType.REWARDED:
                return ShowUnityRewarded();
            default:
                return false;
        }
    }

    public void RegisterProUser()
    {
        PlayerPrefs.SetInt("remove_ads", 1);
        isProUser = true;
        HideBanner();
    }
    public void RewardedAdResult(AdResult adResult)
    {
        if (onAdSuccess != null && adResult == AdResult.SUCCSSEFUL)
        {
            //AnalyticsGame.instance.RewardedADFinished(AdResult.SUCCSSEFUL.ToString());
            onAdSuccess();
            onAdSuccess = null;
            //Achivement_Stats.Instance.DataAdd(Achievement_Type.Vedio_Count, 1);
        }
    }
    #endregion
    //---------------------------------------------- Showing Ad -------------------------------------------------------------------------------------------//


    //---------------------------------------------- Loading Ad -------------------------------------------------------------------------------------------//
    #region Loading Ad
   
   
    public bool IsAdmobrewardLoaded()
    {
        if (CheckInitialization)
        {
            return admobController.IsRewardedAdLoaded();
        }
        else
            return false;
    }
    #endregion
    //---------------------------------------------- Unity Events -----------------------------------------------------------------------------------------------//

    public void OnInitializationComplete()
    {
        LoadAd(interstitialPlacementID);
        LoadAd(URV_PlacementID);
    }
   

    public void OnUnityAdsAdLoaded(string placementId)
    {
        if (placementId == interstitialPlacementID)
        {
            UnityInter = true;
        }
        if (placementId == URV_PlacementID)
        {
            UnityAdLoaded = true;
        }
        //throw new System.NotImplementedException();
    }
   

   
    public void OnUnityAdsShowStart(string placementId)
    {
        //  throw new System.NotImplementedException();
    }
    public void OnUnityAdsShowClick(string placementId)
    {
        // throw new System.NotImplementedException();
    }
    
}
