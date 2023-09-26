using System.Collections;
using UnityEngine;
using SWS;

public class PlayerMove : MonoBehaviour
{
    private Coroutine _cor;
    public Rigidbody Car;
    public float speed;
    public bool moving = false;
    public bool left_right;
    public bool opp;
    public bool up_down;
    public ParticleSystem emoji, emojiCool;
    public splineMove _Waypoint;

    private int _opposite = 1;
    //public AnotherCarDetection CarDetected;

    public LayerMask layer;

    public GameObject OnRoadDetection;
    private RaycastHit _hit;
    private Vector3 _movPos;

    private void Update()
    {
        if (!moving) return;
        
        if (Physics.Raycast(transform.position, _movPos, out _hit, 10f, layer))
        {
            print("RayCast Trigger");
            _Waypoint.Pause();
            //Debug.DrawRay(transform.position, Vector3.forward, Color.green, Mathf.Infinity);
        }
        else
        {
                
            //_Waypoint.StartMove();
            StartCoroutine(Delay());
            //moving = false;
        }

    }

    private IEnumerator Delay()
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

    public void MoveRight()
    {
        _movPos = Vector3.forward;
         StartCoroutine(Right_Left(speed));
    }


    public void MoveLeft()
    {
        _movPos = Vector3.back;
         StartCoroutine(Right_Left(-speed));
    }


    public void MoveUp()
    {
        //transform.DOJump(transform.position, 1.5f, 1, 1f);
    }


    public void MoveDown()
    {
        //transform.DOJump(transform.position, 1.5f, 1, 1f);
    }


    private IEnumerator Right_Left(float moveTowards)
    {

        Car.AddForce(transform.forward * moveTowards, ForceMode.VelocityChange);

        yield return new WaitForSecondsRealtime(0f);
        if(!moving)
            _cor = StartCoroutine(Right_Left(moveTowards));
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
        if (!collision.gameObject.CompareTag("Player")) return;
        if(_cor!=null)
        {
            emoji.Play();
            StopCoroutine(_cor);
            Car.isKinematic = true;
            if(collision.rigidbody != null)
                collision.rigidbody.isKinematic = true;
            SoundsManager.instance.PlayHitSound(SoundsManager.instance.AS);
        }
        _cor = null;
        Car.mass = 1000;
    }
}
