using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sitting_Script : MonoBehaviour
{
    public static Sitting_Script instance;
    public NavMeshAgent[] Passengers;
    public Transform Destination;
    bool Once;
    Coroutine Co;
    public GameObject NextPoint;
    //public GameObject ActionCam;
    //public Camera RccCam;
    private void Awake()
    {
        instance = this;
        foreach (NavMeshAgent agent in Passengers)
        {
            agent.destination = Destination.position;
            agent.isStopped = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Gameplay.instance.fadeOut();
            Gameplay.instance.GameplayPanel.SetActive(false);
            RCC_SceneManager.Instance.activePlayerVehicle.transform.position = this.transform.position;
            RCC_SceneManager.Instance.activePlayerVehicle.transform.rotation = this.transform.rotation;
            RCC_SceneManager.Instance.activePlayerVehicle.rigid.drag = 50;
            //Bus_Controls.instance.DoorFun();
            //foreach (NavMeshAgent agent in Passengers)
            //{
            //    agent.isStopped = false;
            //    agent.gameObject.GetComponent<Animator>().SetInteger("state", 1);
            //}
            StartCoroutine(CheckDistance());
            StartCoroutine(StartMoving());

            //Gameplay.instance.RccCamera.enabled = false;
            //Gameplay.instance.ActionCam.enabled = true;
        }
    }
    IEnumerator CheckDistance()
    {
        yield return new WaitForSecondsRealtime(0.25f);
        foreach (NavMeshAgent agent in Passengers)
        {
           if(Vector3.Distance(agent.transform.position, Destination.position) < 0.1f)
            {
                agent.gameObject.SetActive(false);
            }
        }
        Co = StartCoroutine(CheckDistance());
    }

    IEnumerator StartMoving()
    {
        yield return new WaitForSeconds(15);
        Gameplay.instance.fadeOut();
        Gameplay.instance.GameplayPanel.SetActive(true);
        //Bus_Controls.instance.DoorFun();
        StopCoroutine(Co);
        RCC_SceneManager.Instance.activePlayerVehicle.rigid.drag = 0.01f;
        this.transform.parent.gameObject.SetActive(false);

        //Gameplay.instance.RccCamera.enabled = true;
        //Gameplay.instance.ActionCam.enabled = false;

        NextPoint.SetActive(true);
    }
}
