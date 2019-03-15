﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AI_PlayerFinder : MonoBehaviour {
	[SerializeField]Transform player;
	[SerializeField]NavMeshAgent navAgent;
	[SerializeField]Rigidbody rb;
	[SerializeField]float agentInfluence;
	[SerializeField]float maxRad;
	[SerializeField]float maxMag;
	[SerializeField]AI_EnemyAttack_Melee aiAttack;
	bool isAiming;
	//private int playerInstanceCount;
	// Use this for initialization
	void Start () {
		navAgent.SetDestination(player.position);
		navAgent.updatePosition=false;
		isAiming = true;
		aiAttack.SetTarget(this);
	}
	
	// Update is called once per frame
	void Update () {
		navAgent.SetDestination(player.position);
		if(navAgent.remainingDistance>=navAgent.stoppingDistance){
			if(!aiAttack.GetIsAttacking()){
				navAgent.updateRotation = true;
				aiAttack.StopAttack();
				navAgent.isStopped = false;
			}
		}else{
			if(isAiming&&!aiAttack.GetIsAttacking()){
				navAgent.isStopped = true;
			//Debug.Log(navAgent.remainingDistance);
				navAgent.updateRotation = false;
				Quaternion targetDir =
				Quaternion.LookRotation(new Vector3(player.position.x,transform.position.y,player.position.z)
				-transform.position,Vector3.up);
			
				transform.rotation = Quaternion.RotateTowards(transform.rotation,targetDir,maxRad*Time.deltaTime);
				if(!aiAttack.GetIsAttacking()){
					aiAttack.StartAttack();
				}
			}
		}
		//Debug.Log(navAgent.nextPosition+" compared with "+transform.position);
		rb.AddForce((new Vector3(navAgent.nextPosition.x,transform.position.y,navAgent.nextPosition.z)-transform.position)*agentInfluence);

		

		navAgent.nextPosition = transform.position;
	}

	public Transform GetTarget(){
		return player;
	}
	public NavMeshAgent GetNavMeshAgent(){
		return navAgent;
	}
}
