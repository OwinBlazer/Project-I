using System.Collections;
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
	[SerializeField]AnimationClip backOffAnim;
	[Space]
	[Tooltip("Time required for goblin wait at dash in position")][SerializeField]float stayTime;
	[Tooltip("How far a goblin will back off after attacking, relative to its optimal swarm spot")][SerializeField]float dashOutDistance;
	[Tooltip("The distance in Unity units between a goblin's stop spot and a player's standing spot (since we don't want the goblin to just sit on the player)")][SerializeField]float optimalDistanceFromPlayer;
	[Tooltip("If true, goblin will backdash even when the player has been hit. If not, upon hit goblin returns to original position")][SerializeField]private bool willBackDash;
	[Space]
	[Tooltip("How far from the center of this gameobject will the goblin's swarm distance be")][SerializeField] private Transform maxSwarmDistance;
	[Tooltip("Distance of the goblin to its swarm spot")][SerializeField]private float swarmBuffer;
	[Tooltip("Dictates the speed at which the goblin moves to its swarm spot")][SerializeField]private float swarmSpeed;
	
	private List<int> attackingGoblinIndex = new List<int>();
	private float currentInterval;
	private int attackingQty;
	private int goblinQty;
	private bool isEngaging;
	private bool isAttacking;
	private float swarmSize;
	
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
			tempGoblin.InitializeParameters(startUpTime, player.transform, swarmBuffer,swarmSpeed);
		}
		swarmSize = Mathf.Abs(maxSwarmDistance.localPosition.x)*2;
		UpdateIdealSpot();
	}
	
	// Update is called once per frame
	void Update () {
		
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
				if(currentInterval<attackAnim.length+startUpTime+backOffAnim.length){
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
		for(int i=0;i<allGoblinIndex.Count;i++){
			goblinList[allGoblinIndex[i]].SetSwarmSpot(new Vector3(maxSwarmDistance.localPosition.x+((i+1)*swarmSize/(float)(allGoblinIndex.Count+1)),
																	maxSwarmDistance.localPosition.y,
																	maxSwarmDistance.localPosition.z));
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
		//increase likeliness of higher amounts by using Aggro Level
		attackingQty = (int)Mathf.Floor(Random.Range(1,allGoblinIndex.Count*aggroLevel));
		if(attackingQty>allGoblinIndex.Count){
			attackingQty = allGoblinIndex.Count;
		}

		//lists attacking goblins index, based from living goblins
		for(int i=0;i<attackingQty;i++){
			int rng = Random.Range(0,allGoblinIndex.Count);
			int tempIndex = allGoblinIndex[rng];
			allGoblinIndex.RemoveAt(rng);
			attackingGoblinIndex.Add(tempIndex);
		}

		//Prep by enable dash
		
		foreach(int index in attackingGoblinIndex){
			goblinList[index].DashAtPlayer(attackAnim.length,stayTime,backOffAnim.length,optimalDistanceFromPlayer, dashOutDistance, willBackDash);
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
