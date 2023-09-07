
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreemptANRs : MonoBehaviour
{
	public void Start()
	{
        DontDestroyOnLoad(gameObject);
        var ANRSupervisor = new AndroidJavaClass("ANRSupervisor");
        ANRSupervisor.CallStatic("create");

        // Uncomment if ANRSupervisor should always run, not just during ads.
        ANRSupervisor.CallStatic("start");

	}

    // If your reporting is done in C#, you can use this function to grab the report.
    void Update()
    {

    }

    public void OnTestButtonClicked()
    {

        var ANRSupervisor = new AndroidJavaClass("ANRSupervisor");
        ANRSupervisor.CallStatic("start");

        // Generate an ANR on the main Java thread
        ANRSupervisor.CallStatic("generateANROnMainThreadTEST");

        // Test the reporting function
        //var anrReport = new ANRReport() { callstacks = new List<string>() {
        //    "A",
        //    "B",
        //} };
        //PlayFabManager.Instance.ExeCloudScript("reportANR", new Dictionary<string, object> {
        //    {"report", anrReport},
        //});

        // Generate an ANR on the Unity thread (but that would not cause a Google ANR)
        //for (var i = 0; i < 60; ++i) // ANR!
        //{
        //    System.Threading.Thread.Sleep(1000); // ANR!
        //}

    }
}


