using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickPoint : MonoBehaviour
{
    public static PickPoint instance; 
    public Transform Destination;
    public GameObject NextPoint;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Gameplay.instance.fadeOut();
            Gameplay.instance.GameplayPanel.SetActive(false);
            RCC_SceneManager.Instance.activePlayerVehicle.transform.position = this.transform.position;
            RCC_SceneManager.Instance.activePlayerVehicle.transform.rotation = this.transform.rotation;
            RCC_SceneManager.Instance.activePlayerVehicle.rigid.drag = 50;
            //Gameplay.instance.RccCamera.enabled = false;
            //Gameplay.instance.ActionCam.enabled = true;

            StartCoroutine(StartMoving());

        }
    }

    IEnumerator StartMoving()
    {
        yield return new WaitForSeconds(2f);
        Gameplay.instance.fadeOut();
        Gameplay.instance.GameplayPanel.SetActive(true);
        RCC_SceneManager.Instance.activePlayerVehicle.rigid.drag = 0.01f;
        this.transform.parent.gameObject.SetActive(false);
        //Gameplay.instance.RccCamera.enabled = true;
        //Gameplay.instance.ActionCam.enabled = false;


        NextPoint.SetActive(true);
    }
}
