using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class AttachToBody : MonoBehaviour {
	[SerializeField] Transform bodyObject;
	// Use this for initialization
	void Start () {
		bodyObject = VRTK_DeviceFinder.HeadsetTransform();
		this.transform.SetParent(bodyObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
