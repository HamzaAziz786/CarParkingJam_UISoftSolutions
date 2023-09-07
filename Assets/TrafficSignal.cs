using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrafficSignal : MonoBehaviour
{
    public Text texttype;
    public GameObject Red, Green, Yellow;
    bool IsGreen, IsRed, IsYellow;

    private void Start()
    {
        IsRed = true;
        StartCoroutine(SignalHadler());
    }

    void HideAll()
    {
        Red.SetActive(false);
        Green.SetActive(false);
        Yellow.SetActive(false);
    }

    IEnumerator SignalHadler()
    {
        yield return new WaitForSeconds(5f);
        HideAll();
        if(IsRed)
        {
            IsRed = false;
            IsYellow = true;
            IsGreen = false;
            Yellow.SetActive(true);
        }else if(IsYellow)
        {
            IsRed = false;
            IsYellow = false;
            IsGreen = true;
            Green.SetActive(true);
        }
        else if (IsGreen)
        {
            IsRed = true;
            IsYellow = false;
            IsGreen = false;
            Red.SetActive(true);
        }
        StartCoroutine(SignalHadler());
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(IsRed)
            {
                texttype.text = "You did not followed the Signal.";
                Gameplay.instance.ShowNotification();
            }else
            {
                texttype.text = "You Successfully followed the Signal.";
                Gameplay.instance.ShowNotification();
            }
        }
    }
}
