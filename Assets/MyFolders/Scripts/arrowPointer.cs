using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowPointer : MonoBehaviour
{
  
    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("checkpoint") != null)
        {
            this.transform.LookAt(GameObject.FindGameObjectWithTag("checkpoint").transform);
        }
    }
}
