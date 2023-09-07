using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Setting_Script : MonoBehaviour
{

    public Slider Sound;

    //[Space(10)]
    //[Header("Control Settings")]
    //public GameObject SteeringOn;
    //public GameObject SteeringOff;
    //public GameObject BtnsOn;
    //public GameObject BtnsOff;

    private void OnEnable()
    {
       

        //if(PlayerPrefs.GetInt("control") == 0)
        //{
        //    SteeringOn.SetActive(true);
        //    BtnsOff.SetActive(true);
        //    SteeringOff.SetActive(false);
        //    BtnsOn.SetActive(false);
        //    RCC_Settings.Instance.mobileController = RCC_Settings.MobileController.SteeringWheel;
        //}
        //else
        //{
        //    SteeringOn.SetActive(false);
        //    BtnsOff.SetActive(false);
        //    SteeringOff.SetActive(true);
        //    BtnsOn.SetActive(true);
        //    RCC_Settings.Instance.mobileController = RCC_Settings.MobileController.TouchScreen;
        //}

    }
    //public void ChangeControl()
    //{
    //    if (PlayerPrefs.GetInt("control") == 0)
    //    {
    //        SteeringOn.SetActive(false);
    //        BtnsOff.SetActive(false);
    //        SteeringOff.SetActive(true);
    //        BtnsOn.SetActive(true);
    //        RCC_Settings.Instance.mobileController = RCC_Settings.MobileController.TouchScreen;
    //        PlayerPrefs.SetInt("control", 1);
    //    }
    //    else
    //    {
    //        SteeringOn.SetActive(true);
    //        BtnsOff.SetActive(true);
    //        SteeringOff.SetActive(false);
    //        BtnsOn.SetActive(false);
    //        RCC_Settings.Instance.mobileController = RCC_Settings.MobileController.SteeringWheel;
    //        PlayerPrefs.SetInt("control", 0);
    //    }
    //}


    private void Update()
    {
        AudioListener.volume = Sound.value;
    }





}
