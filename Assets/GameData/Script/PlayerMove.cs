using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using SWS;

public class PlayerMove : MonoBehaviour
{
    public Coroutine cor;
    public Rigidbody Car;
    public float speed;
    public bool moving = false;
    public bool left_right;
    public bool opp;
    public bool up_down;
    public ParticleSystem emoji, emojiCool;
    public splineMove _Waypoint;
    int _opposit = 1;
    //public AnotherCarDetection CarDetected;

    public LayerMask layer;

    public GameObject OnRoadDetection;
    RaycastHit hit;
    Vector3 mov_pos;

    private void Update()
    {
        if (moving)
        {            
            if (Physics.Raycast(transform.position, mov_pos, out hit, 10f, layer))
            {
                print("RayCast Trigger");
                _Waypoint.Pause();
                //Debug.DrawRay(transform.position, Vector3.forward, Color.green, Mathf.Infinity);
            }
            else
            {
                
                //_Waypoint.StartMove();
                StartCoroutine(delay());
                //moving = false;
            }
        }
        
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(0.1f);
        moving = false;
        yield return new WaitForSeconds(0.5f);
        //if(_Waypoint.IsPaused())
        _Waypoint.Pause();
        _Waypoint.Resume(); 
    }

    private void Start()
    {
        if (opp)
        {
            speed *= -1;
        }

    }

    public void moveRight()
    {
        mov_pos = Vector3.forward;
         StartCoroutine(Right_Left(speed));
    }


    public void moveLeft()
    {
        mov_pos = Vector3.back;
         StartCoroutine(Right_Left(-speed));
    }


    public void moveUp()
    {
        //transform.DOJump(transform.position, 1.5f, 1, 1f);
    }


    public void moveDown()
    {
        //transform.DOJump(transform.position, 1.5f, 1, 1f);
    }

    
    IEnumerator Right_Left(float movetowards)
    {

        Car.AddForce(transform.forward * movetowards, ForceMode.VelocityChange);

        yield return new WaitForSecondsRealtime(0f);
        if(!moving)
        cor = StartCoroutine(Right_Left(movetowards));
    }


    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Exit"))
        {
            emojiCool.Play();
        }
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(cor!=null)
            {
                emoji.Play();
                StopCoroutine(cor);
                Car.isKinematic = true;
                if(collision.rigidbody != null)
                collision.rigidbody.isKinematic = true;
                SoundsManager.instance.PlayHitSound(SoundsManager.instance.AS);
            }
            cor = null;
            Car.mass = 1000;
        }
    }
}
