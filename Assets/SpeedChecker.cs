using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedChecker : MonoBehaviour
{
    public float Speed;
    public Text Message;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (RCC_SceneManager.Instance.activePlayerVehicle.speed <= Speed)
            {
                Message.text = "You Successfully followed the rule.";
                Gameplay.instance.ShowNotification();
                this.GetComponent<BoxCollider>().enabled = false;
            }else
            {
                Message.text = "You did not followed the rule.";
                Gameplay.instance.ShowNotification();
                this.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}
