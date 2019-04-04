using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Test_NavFind : MonoBehaviour {
    [SerializeField] Transform target;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Rigidbody rb;
    bool isAiming;
    private bool isRunning;
    private bool isAttacking;
    // Use this for initialization
    void Start () {
        agent.SetDestination(target.position);
        isAiming = true;
        isRunning = true;
        isAttacking = false;
        agent.updatePosition = false;
    }
	
	// Update is called once per frame
	void Update () {
        
        //Debug.Log(agent.nextPosition+" compared with "+transform.position);
        if (isRunning)
        {
            rb.AddForce((new Vector3(agent.nextPosition.x, transform.position.y, agent.nextPosition.z) - transform.position) * 2000);
        }

        agent.nextPosition = transform.position;
    }
    private bool GetIsAttacking()
    {
        return isAttacking;
    }

    private void StartAttack()
    {
        isAttacking = true;
    }
    private void StopAttack()
    {
        isAttacking = false;
    }
}
