using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headLights : MonoBehaviour
{
    void Start()
    {
        InvokeRepeating("Check", 0.1f, 1f);
    }

    void Check()
    {
        //if (TOD_Sky.Instance.Cycle.Hour > 19 || TOD_Sky.Instance.Cycle.Hour < 6 || RainHandler.instance.IsRain)//DayNightController.instance.isLights)
        //{
        //    this.gameObject.SetActive(true);
        //    //Green_Mat.mainTexture = GreenNight;
        //    //Snow_Mat.mainTexture = SnowNight;
        //    //Desert_Mat.mainTexture = DesertNight;
        //}
        //else
        //{
        //    this.gameObject.SetActive(false);
        //    //Green_Mat.mainTexture = GreenDay;
        //    //Snow_Mat.mainTexture = SnowDay;
        //    //Desert_Mat.mainTexture = DesertDay;
        //}
    }
}
