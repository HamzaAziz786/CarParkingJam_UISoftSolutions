using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopChecker : MonoBehaviour
{
    public Text texttype;
    bool IsStopped;

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(RCC_SceneManager.Instance.activePlayerVehicle.speed <= 1)
            {
                IsStopped = true;
                texttype.text = "You Successfully followed the rule.";
                Gameplay.instance.ShowNotification();
                this.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           if(!IsStopped)
            {
                texttype.text = "You did not followed the rule.";
                Gameplay.instance.ShowNotification();
                this.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}
