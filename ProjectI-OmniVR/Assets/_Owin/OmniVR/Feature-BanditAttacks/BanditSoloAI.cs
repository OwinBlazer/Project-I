using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditSoloAI : MonoBehaviour {
	[SerializeField]GameObject player;
	[SerializeField]Animator anim;
	[Space]
	[SerializeField]float startUpTime;
	private float currStartUpTime;
	[SerializeField]float attackInterval;
	[SerializeField]AnimationClip attackAnim;
	[SerializeField]AnimationClip backOffAnim;
	[Space]
	[Tooltip("Time required for bandit wait at dash in position")][SerializeField]float stayTime;
	[Tooltip("How far a bandit will back off after attacking, relative to its optimal swarm spot")][SerializeField]float dashOutDistance;
	[Tooltip("The distance in Unity units between a bandit's stop spot and a player's standing spot (since we don't want the goblin to just sit on the player)")][SerializeField]float optimalDistanceFromPlayer;
	[Tooltip("If true, bandit will backdash even when the player has been hit. If not, upon hit goblin returns to original position")][SerializeField]private bool willBackDash;
	private float currentInterval;
	private int attackingQty;
	private bool isEngaging;
	private bool isAttacking;
	private PlayerAimer aimer;
	
	// Use this for initialization
	void Start () {
		InitializeParameter();
	}
	
	public void InitializeParameter(){anim = GetComponent<Animator>();
		isEngaging=false;
		isAttacking=false;
		aimer = GetComponentInChildren<PlayerAimer>();
		currentInterval = 0;
		currStartUpTime = 0;
	}
	// Update is called once per frame
	void Update () {
		
		if(isEngaging){
			//engage means bandit is already in position
			if(!isAttacking){
				// !Attacking means bandit is still in cooldown
				if(currentInterval<attackInterval){
					currentInterval+=Time.deltaTime;
				}else{
					if(currStartUpTime==0){
						PrepareAttack();
						currStartUpTime+=Time.deltaTime;
					}else if(currStartUpTime<startUpTime){
						currStartUpTime+=Time.deltaTime;
					}else{
						isAttacking = true;
						currentInterval = 0;
						currStartUpTime = 0;
					}
				}
			}else{
				//the bandit is now attacking
				if(currentInterval==0){
					currentInterval+=Time.deltaTime;
					//start ATTACKING animation
					//control using animation to transition to backing off 
					aimer.TriggerDash();
					anim.SetInteger("ActID",2);

				}else if(currentInterval<attackAnim.length+startUpTime+backOffAnim.length){
					currentInterval+=Time.deltaTime;
				}else{
					currentInterval = 0;
					isAttacking = false;
				}
			}
		}
	}
	
	private void PrepareAttack(){
		//change animation to PREPARE animation 
		anim.SetInteger("ActID",1);

		//Prep by enable dash
		aimer.DashAtPlayer(attackAnim.length,stayTime,backOffAnim.length,optimalDistanceFromPlayer, dashOutDistance, willBackDash);

		//prep By Aiming
		aimer.RotateAt(player.transform);
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
	public void StartAttacking(){
		isEngaging = true;
	}
	public void attackIntervalReset(){
		currentInterval = 0;
	}
}
