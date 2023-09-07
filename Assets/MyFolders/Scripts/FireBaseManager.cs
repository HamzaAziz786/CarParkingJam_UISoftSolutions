using System;
using UnityEngine;
using Firebase;
using Firebase.Analytics;


public class FireBaseManager : MonoBehaviour
{

    DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;//@@@@testing
    public bool ShowDebugLogs;

    public static FireBaseManager Instance;
   
    
    private FirebaseApp app;

   
    void Awake()
	{

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

    }

public void Start()
    {
        //firebase must be Initilized on start
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
       
    }
    

    #region firebase Initilization 

    /////uncomment if you're using firebase Analytics  
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

      
    }

    ///trigger firebase(Analytics) custom log Event with single parameter
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


    #endregion
    //***************************** FireBase *****************************************************************************
    #region firebase RemoteConfig

    //public void DisplayAllKeys()
    //{
    //    if (ShowDebugLogs)
    //        Debug.Log("Current Keys:");
    //    System.Collections.Generic.IEnumerable<string> keys =
    //        Firebase.RemoteConfig.FirebaseRemoteConfig.Keys;
    //    foreach (string key in keys)
    //    {
    //        if (ShowDebugLogs)
    //            Debug.Log("    " + key);
    //    }
    //    if (ShowDebugLogs)
    //        Debug.Log("GetKeysByPrefix(\"config_test_s\"):");
    //    keys = Firebase.RemoteConfig.FirebaseRemoteConfig.GetKeysByPrefix("config_test_s");
    //    foreach (string key in keys)
    //    {
    //        if (ShowDebugLogs)
    //            Debug.Log("    " + key);
    //    }
    //}
    //private bool _isArrayNullOrEmpty(StringValues[] stringValues)
    //{
    //    return (stringValues == null || stringValues.Length == 0);
    //}

    //private bool _isArrayNullOrEmpty(FloatValues[] floatValues)
    //{
    //    return (floatValues == null || floatValues.Length == 0);
    //}
    //private bool _isArrayNullOrEmpty(BoleanValues[] boleanValues)
    //{
    //    return (boleanValues == null || boleanValues.Length == 0);
    //}

    //public Task FetchDataAsync()
    //{
    //    // DebugLog("Fetching data...");
    //    // FetchAsync only fetches new data if the current data is older than the provided
    //    // timespan.  Otherwise it assumes the data is "recent enough", and does nothing.
    //    // By default the timespan is 12 hours, and for production apps, this is a good
    //    // number.  For this example though, it's set to a timespan of zero, so that
    //    // changes in the console will always show up immediately.
    //    System.Threading.Tasks.Task fetchTask = Firebase.RemoteConfig.FirebaseRemoteConfig.FetchAsync(
    //        TimeSpan.Zero);
    //    return fetchTask.ContinueWith(FetchComplete);
    //}

    //void FetchComplete(Task fetchTask)
    //{
    //    if (fetchTask.IsCanceled)
    //    {
    //        if (ShowDebugLogs)
    //            Debug.Log("Fetch canceled.");
    //    }
    //    else if (fetchTask.IsFaulted)
    //    {
    //        if (ShowDebugLogs)
    //            Debug.Log("Fetch encountered an error.");
    //    }
    //    else if (fetchTask.IsCompleted)
    //    {

    //        if (ShowDebugLogs)
    //            Debug.Log("Fetch completed successfully!");
    //        getData();
    //    }

    //    var info = Firebase.RemoteConfig.FirebaseRemoteConfig.Info;
    //    switch (info.LastFetchStatus)
    //    {
    //        case Firebase.RemoteConfig.LastFetchStatus.Success:
    //            Firebase.RemoteConfig.FirebaseRemoteConfig.ActivateFetched();
    //            getData();
    //            if (ShowDebugLogs)
    //                Debug.Log(String.Format("Remote data loaded and ready (last fetch time {0}).", info.FetchTime));
    //            break;
    //        case Firebase.RemoteConfig.LastFetchStatus.Failure:
    //            switch (info.LastFetchFailureReason)
    //            {
    //                case Firebase.RemoteConfig.FetchFailureReason.Error:
    //                    if (ShowDebugLogs)
    //                        Debug.Log("Fetch failed for unknown reason");
    //                    break;
    //                case Firebase.RemoteConfig.FetchFailureReason.Throttled:
    //                    if (ShowDebugLogs)
    //                        Debug.Log("Fetch throttled until " + info.ThrottledEndTime);
    //                    break;
    //            }
    //            break;
    //        case Firebase.RemoteConfig.LastFetchStatus.Pending:
    //            if (ShowDebugLogs)
    //                Debug.Log("Latest Fetch call still pending.");
    //            break;
    //    }
    //}


    //void getData()
    //{
    //    if (dependencyStatus == DependencyStatus.Available)
    //    {
    //        try
    //        {
    //            if (!_isArrayNullOrEmpty(_FloatValues))
    //            {
    //                foreach (var item in _FloatValues)
    //                {
    //                    item.value = (float)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(item.Name).DoubleValue;
    //                    if (item.Name == delayed_Interstitial_key) {
    //                        delayed_Interstitial_value = item.value;
    //                    }
    //                }
    //            }


    //            if (!_isArrayNullOrEmpty(_StringValues))
    //            {
    //                foreach (var item in _StringValues)
    //                {
    //                    item.value = Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(item.Name).StringValue;
    //                }
    //            }

    //            if (!_isArrayNullOrEmpty(_BoleanValues))
    //            {
    //                foreach (var item in _BoleanValues)
    //                {
    //                    item.value = Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(item.Name).BooleanValue;
    //                }
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            if (ShowDebugLogs)
    //            {
    //                Debug.Log("Exception Generated" + ex);
    //            }
    //        }
    //    }
    //}

    //public void moveBannerToBottom(bool isBottom) {
    //    if(isBottom)
    //    ShowBannerReposition(AdPosition.Bottom);
    //    else
    //        ShowBannerReposition(AdPosition.Top);

    //}

    #endregion

}

