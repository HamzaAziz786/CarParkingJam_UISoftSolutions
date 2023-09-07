using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights_Handler : MonoBehaviour
{
    public GameObject[] brakeLights;
    public GameObject singal_left, signal_right;
    // Start is called before the first frame update
    void Start()
    {
        if (SystemInfo.systemMemorySize > 3072)
        {
            InvokeRepeating("CheckLights", 0.25f, 0.25f);
        }
        else
        {
            signal_right.SetActive(false);
            singal_left.SetActive(false);

            foreach (GameObject obj in brakeLights)
                obj.SetActive(false);

        }
    }

    void CheckLights()
    {

        //if (Gameplay.instance.myPlayer.steerInput > 0.15f)
        //{
        //    signal_right.SetActive(false);
        //    singal_left.SetActive(true);
        //}
        //else if (Gameplay.instance.myPlayer.steerInput < -0.15f)
        //{
        //    signal_right.SetActive(true);
        //    singal_left.SetActive(false);
        //}
        //else
        //{
        //    signal_right.SetActive(false);
        //    singal_left.SetActive(false);
        //}
        if (Gameplay.instance.myPlayer.brakeInput > 0.01f)
        {
            foreach (GameObject obj in brakeLights)
                obj.SetActive(true);
        }
        else
        {
            foreach (GameObject obj in brakeLights)
                obj.SetActive(false);
        }
    }
}
