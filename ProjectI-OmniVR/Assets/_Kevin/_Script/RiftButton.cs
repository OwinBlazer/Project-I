using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            Debug.Log("BtnLeftTrigger");
        }
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            Debug.Log("BtnRightTrigger");
        }

        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            Debug.Log("BtnA");
        }
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            Debug.Log("BtnX");
        }

        if (OVRInput.GetDown(OVRInput.RawButton.LThumbstick))
        {
            Debug.Log("BtnLeftStickPressed");
        }
        if (OVRInput.GetDown(OVRInput.RawButton.RThumbstick))
        {
            Debug.Log("BtnRightStickPressed");
        }

        if (OVRInput.GetDown(OVRInput.RawButton.RThumbstickUp))
        {
            Debug.Log("RThumbstickUp");
        }
        if (OVRInput.GetDown(OVRInput.RawButton.RThumbstickLeft))
        {
            Debug.Log("RThumbstickLeft");
        }
    }
}
