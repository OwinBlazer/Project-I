using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class GoblinSoloAI : MonoBehaviour {
	[SerializeField]Animator anim;
	[SerializeField]PlayerAimer aimer;
	[SerializeField]NavMeshAgent navAgent;
	private float waitTime;
	private float currWaitTime;
	private Transform player;
	private float swarmBuffer;
	private Vector3 swarmSpot;
	private float swarmSpeed;
	//ActID legend ==> 0 = idle | 1 = aim | 2 = attack

	// Use this for initialization
	void Start () {
		currWaitTime = 0;
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		//going to ideal spot
		if((swarmSpot-transform.position).sqrMagnitude>swarmBuffer*swarmBuffer){
			if(anim.GetInteger("ActID")!=1){
				navAgent.SetDestination(swarmSpot);
				navAgent.isStopped = false;
			}
		}else{
			if(anim.GetInteger("ActID")==0){
				aimer.RotateAt(player);
			}
			navAgent.isStopped = true;
		}

		if(anim.GetInteger("ActID")==1){
			//if prepping for attack, wait for start up time, then attack
			if(currWaitTime<waitTime){
				currWaitTime+=Time.deltaTime;
			}else{
				LaunchAttack();
				currWaitTime = 0;
			}
		}

	}

	public void LaunchAttack(){
		//start attack
		anim.SetInteger("ActID",2);
		aimer.TriggerDash();
	}

	public void PrepareAttack(){
		//aim attack
		anim.SetInteger("ActID",1);
		aimer.RotateAt(player);
	}

	public void DashAtPlayer(float dashIn, float stayDur, float dashOut, float distanceFromPlayer, float dashOutDistance, bool willBackDash){
		aimer.DashAtPlayer(dashIn, stayDur, dashOut, distanceFromPlayer,dashOutDistance, willBackDash);
	}
	public void FinishAttack(){
		//Goes to backing off anim
		anim.SetInteger("ActID",3);
	}
	public void ReturnToIdle(){
		//Resets anim
		anim.SetInteger("ActID",0);
	}
	public void AttackHasHit(){
		aimer.AttackHasHit();
	}
	public void InitializeParameters(float waitTime, Transform player, float swarmBuffer, float swarmSpeed){
		this.waitTime = waitTime;
		this.player = player;
		this.swarmBuffer = swarmBuffer;
		this.swarmSpeed = swarmSpeed;
	}
	public void SetSwarmSpot(Vector3 newSpot){
		swarmSpot = newSpot;
	}
}
