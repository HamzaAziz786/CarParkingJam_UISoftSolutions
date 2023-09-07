using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode] 
public class RotateObject : MonoBehaviour {

	public bool Xaxis,Yaxis,Zaxis;
	public bool IsInUpdate;
	public float speed;
	public bool IsInFixedUpdate;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (IsInUpdate) {
			if (Xaxis) {
				transform.Rotate (speed*Time.deltaTime,0,0);
			}
			if (Yaxis) {
				transform.Rotate (0,speed*Time.deltaTime,0);
			}
			if (Zaxis) {
				transform.Rotate (0,0,speed*Time.deltaTime);
			}
		}
	}

	void FixedUpdate(){
		if (IsInFixedUpdate) {
			if (Xaxis) {
				transform.Rotate (speed*Time.deltaTime,0,0);
			}
			if (Yaxis) {
				transform.Rotate (0,speed*Time.deltaTime,0);
			}
			if (Zaxis) {
				transform.Rotate (0,0,speed*Time.deltaTime);
			}
		}
	}
}
