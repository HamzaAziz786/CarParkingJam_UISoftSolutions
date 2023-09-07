using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPoint : MonoBehaviour
{

    public GameObject NextPoint;
    public bool IsLast;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Gameplay.instance.fadeOut();
            Gameplay.instance.GameplayPanel.SetActive(false);
            RCC_SceneManager.Instance.activePlayerVehicle.transform.position = this.transform.position;
            RCC_SceneManager.Instance.activePlayerVehicle.transform.rotation = this.transform.rotation;
            RCC_SceneManager.Instance.activePlayerVehicle.rigid.isKinematic = true;
            //Gameplay.instance.RccCamera.enabled = false;
            //Gameplay.instance.ActionCam.enabled = true;

            StartCoroutine(ActivePassenger());


        }
    }

    IEnumerator ActivePassenger()
    {

        Gameplay.instance.Confetti.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        StartMoving();
    }

    void StartMoving()
    {
       Gameplay.instance.OnComplete();
    }
}
