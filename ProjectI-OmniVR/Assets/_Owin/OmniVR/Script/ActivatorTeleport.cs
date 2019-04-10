using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorTeleport : MonoBehaviour {
	[SerializeField]GameObject activationTarget;
	// Use this for initialization
	private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            activationTarget.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            activationTarget.SetActive(false);
        }
    }
}
