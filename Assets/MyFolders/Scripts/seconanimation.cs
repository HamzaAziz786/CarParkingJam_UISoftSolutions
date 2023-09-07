using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class seconanimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            for (int i = 0; i < Store_Script.instance.players.Length; i++)
            {
                Store_Script.instance.players[i].gameObject.GetComponent<Animator>().SetInteger("state", 1);
            }
        }
    }
}
