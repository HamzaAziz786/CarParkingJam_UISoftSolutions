using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject NextPoint;
    public AudioClip Clip;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(NextPoint != null)
                NextPoint.SetActive(true);

            Gameplay.instance.BtnClickSource.PlayOneShot(Clip);
            this.gameObject.SetActive(false);
            
        }
    }
}
