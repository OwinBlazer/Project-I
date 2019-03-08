﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinPlatoonHandler : MonoBehaviour {
	[SerializeField]GameObject player;
	[Tooltip("This list is populated at Start() based on children")][SerializeField]List<GoblinSoloAI> goblinList = new List<GoblinSoloAI>();
	[Space]
	[SerializeField]float startUpTime;
	private float currStartUpTime;
	[Tooltip("The tendency for more goblins to attack at once. 0 is none of them will attack. 1 is all of them may attack. more than that increases the likelyhood of all 5 attacking at once")][SerializeField]float aggroLevel;
	[SerializeField]float attackInterval;
	[SerializeField]AnimationClip attackAnim;
	[Space]
	[Tooltip("Time required for goblin to jump into attack position. Adjust with animation.")][SerializeField]float dashInTime;
	[Tooltip("Time required for goblin wait at dash in position. Adjust with animation.")][SerializeField]float stayTime;
	[Tooltip("Time required for goblin to jump back. Adjust with animation.")][SerializeField]float dashOutTime;
	[Tooltip("The distance un Unity units between a goblin's stop spot and a player's standing spot (since we don't want the goblin to just sit on the player)")][SerializeField]float optimalDistanceFromPlayer;
	[SerializeField] private Transform maxSwarmDistance;
	private List<int> attackingGoblinIndex = new List<int>();
	private float currentInterval;
	private int attackingQty;
	private int goblinQty;
	private bool isEngaging;
	private bool isAttacking;
	private List<Vector3> IdealSwarmSpot = new List<Vector3>();
	private float SwarmSize;
	
	// Use this for initialization
	void Start () {
		isEngaging=false;
		isAttacking=false;
		//debug@@@@@@
		isEngaging = true;
		//-------------------------
		currentInterval = 0;
		currStartUpTime = 0;
		goblinQty = transform.childCount;
		for(int i=0;i<transform.childCount;i++){
			GoblinSoloAI tempGoblin = transform.GetChild(i).GetComponent<GoblinSoloAI>();
			goblinList.Add(tempGoblin);
			tempGoblin.InitializeParameters(startUpTime, player.transform);
		}
		SwarmSize = Mathf.Abs(maxSwarmDistance.localPosition.x);
	}
	
	// Update is called once per frame
	void Update () {
		//@@@@@make goblins run to their ideal spots
		//divide swarm size into spots via goblin quantity
		
		if(isEngaging){
			//engage means goblin platoon is already in position
			if(!isAttacking){
				//!Attacking means goblin is still in cooldown
				if(currentInterval<attackInterval){
					currentInterval+=Time.deltaTime;
				}else{
					//goblin solo AI controls start up duration and returns to position via animation
					PrepareAttack();
					isAttacking = true;
					currentInterval = 0;
				}
			}else{
				//goblin is waiting for animation finish
				if(currentInterval<attackAnim.length+startUpTime){
					currentInterval+=Time.deltaTime;
				}else{
					currentInterval = 0;
					isAttacking = false;
				}
			}
		}
	}
	private void UpdateIdealSpot(){
		//check the number of goblins alive@@@
		List<int> allGoblinIndex = new List<int>();
		//lists living goblins index@@@@@
		for(int i=0;i<goblinQty;i++){
			//if isLiving@@@@@@
			allGoblinIndex.Add(i);
		}
		
	}
	private void PrepareAttack(){
		List<int> allGoblinIndex = new List<int>();
		attackingGoblinIndex.Clear();

		//lists living goblins index@@@@@
		for(int i=0;i<goblinQty;i++){
			//if isLiving@@@@@@
			allGoblinIndex.Add(i);
		}

		//choose attacking number of goblins
		//increase likeliness of higher amounts by using Aggro Level @@@@@@@@@@@
		attackingQty = Random.Range(1,allGoblinIndex.Count);

		//lists attacking goblins index, based from living goblins
		for(int i=0;i<attackingQty;i++){
			int rng = Random.Range(0,allGoblinIndex.Count);
			int tempIndex = allGoblinIndex[rng];
			allGoblinIndex.RemoveAt(rng);
			attackingGoblinIndex.Add(tempIndex);
		}

		//Prep by enable dash
		
		foreach(int index in attackingGoblinIndex){
			goblinList[index].DashAtPlayer(dashInTime,stayTime,dashOutTime,optimalDistanceFromPlayer);
		}

		//prep By Aiming
		foreach(int index in attackingGoblinIndex){
			goblinList[index].PrepareAttack();
		}
	}

	public void StartAttacking(){
		isEngaging = true;
	}
	public void attackIntervalReset(){
		currentInterval = 0;
	}
}
