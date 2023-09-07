using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignMessage : MonoBehaviour
{
   
    public AudioSource sound;
    public string textmessage;
    public Text texttype;


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            //if (RCC_SceneManager.Instance.activePlayerVehicle.speed > 20)
            //{
            texttype.text = textmessage;
            sound.Play();
            Gameplay.instance.ShowNotification();
            //}
        }
    }

}
