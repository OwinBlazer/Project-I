using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DistanceTracker : MonoBehaviour {

    public Vector3 runDistance;
    public Vector3 deltaMovement;
    public Vector3 lastPos;

    NavMeshAgent nav;

	// Use this for initialization
	void Start ()
    {
        nav = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            nav.SetDestination(transform.forward) ;
        }
    }
}
