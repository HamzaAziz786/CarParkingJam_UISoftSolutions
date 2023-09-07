using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Droping_Script : MonoBehaviour
{
    public NavMeshAgent[] Passengers;
    public Transform[] Destination;
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
            RCC_SceneManager.Instance.activePlayerVehicle.rigid.drag = 50;
            //Gameplay.instance.RccCamera.enabled = false;
            //Gameplay.instance.ActionCam.enabled = true;

            StartCoroutine(ActivePassenger());
        }
    }
    IEnumerator ActivePassenger()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        Passengers[0].gameObject.SetActive(true);
        Passengers[0].destination = Destination[Random.Range(0, Destination.Length)].position;
        yield return new WaitForSecondsRealtime(1.5f);
        Passengers[1].gameObject.SetActive(true);
        Passengers[1].destination = Destination[Random.Range(0, Destination.Length)].position;
        yield return new WaitForSecondsRealtime(1.5f);
        Passengers[2].gameObject.SetActive(true);
        Passengers[2].destination = Destination[Random.Range(0, Destination.Length)].position;
        yield return new WaitForSecondsRealtime(1.5f);
        Passengers[3].gameObject.SetActive(true);
        Passengers[3].destination = Destination[Random.Range(0, Destination.Length)].position;

        yield return new WaitForSecondsRealtime(5f);
        StartMoving();
    }

    void StartMoving()
    {
        Gameplay.instance.fadeOut();
        Gameplay.instance.GameplayPanel.SetActive(true);
        //Bus_Controls.instance.DoorFun();
        RCC_SceneManager.Instance.activePlayerVehicle.rigid.drag = 0.01f;
        this.transform.parent.gameObject.SetActive(false);

        //Gameplay.instance.RccCamera.enabled = true;
        //Gameplay.instance.ActionCam.enabled = false;

        if (!IsLast)
            NextPoint.SetActive(true);
        else
            Gameplay.instance.OnComplete();
    }
}
