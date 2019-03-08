using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class AttachToHead : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Transform tempTrans = 
			VRTK_DeviceFinder.HeadsetTransform();
		transform.position =  tempTrans.position;
		transform.rotation =  tempTrans.rotation;
	}
}
