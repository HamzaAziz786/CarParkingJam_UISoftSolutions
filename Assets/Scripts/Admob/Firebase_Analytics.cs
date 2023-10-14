using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Analytics;
using System;

public class Firebase_Analytics : MonoBehaviour
{
    DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
    public bool ShowDebugLogs;

    public static Firebase_Analytics Instance;


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
}
