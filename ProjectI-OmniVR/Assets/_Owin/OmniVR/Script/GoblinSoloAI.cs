using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinSoloAI : MonoBehaviour {
	[SerializeField]Animator anim;
	[SerializeField]AnimationClip attackClip;
	[SerializeField]PlayerAimer aimer;
	private float waitTime;
	private float currWaitTime;
	private Transform player;
	//ActID legend ==> 0 = idle | 1 = aim | 2 = attack

	// Use this for initialization
	void Start () {
		currWaitTime = 0;
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
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

	public void DashAtPlayer(float dashIn, float stayDur, float dashOut, float distanceFromPlayer){
		aimer.DashAtPlayer(dashIn, stayDur, dashOut, distanceFromPlayer);
	}
	public void FinishAttack(){
		//resets anim
		anim.SetInteger("ActID",0);
	}
	public void InitializeParameters(float waitTime, Transform player){
		this.waitTime = waitTime;
		this.player = player;

	}
}
