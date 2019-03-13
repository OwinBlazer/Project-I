using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimer : MonoBehaviour {
	[SerializeField]float rotationSpeed;
	[SerializeField]float buffer;
	[SerializeField]Transform targetTransform;
	private bool canDash;
	private bool isDashing;
	bool isAiming;
	private Vector3 optimalSpot;
	private Vector3 backDashSpot;
	private float dashIn, stayDur, backOff, distanceFromPlayer, dashTimer, movDist, backOffDist;
	private bool willBackDash;
	private float backDashMoveTime;
	private void Start(){
		isAiming = false;
		dashTimer = 0;
		canDash = false;
		isDashing = false;
	}
	public void RotateAt(Transform target){
		isAiming=true;
		targetTransform = target;
		if(canDash){
			movDist = Vector3.Distance(targetTransform.position,transform.position);
			//find optimal spot for dashing into
			Vector3 distanceVector = targetTransform.position-transform.position;
			optimalSpot = transform.position +  (new Vector3(distanceVector.x,0,distanceVector.z))* (movDist-distanceFromPlayer)/movDist;
			backDashSpot = transform.parent.position+(transform.parent.position-optimalSpot).normalized*backOffDist;
			backDashMoveTime = backOffDist+ movDist-distanceFromPlayer;
		}
	}

	public void Update(){
		if(isAiming){
			Quaternion targetDir =
			Quaternion.LookRotation(new Vector3(transform.position.x,0,transform.position.z)
			-new Vector3(targetTransform.position.x,0,targetTransform.position.z),Vector3.up);
			
			transform.rotation = Quaternion.RotateTowards(transform.rotation,targetDir,rotationSpeed*Time.deltaTime);

			if(BufferedFloatEqual(targetDir.eulerAngles.y,transform.rotation.eulerAngles.y,buffer)){
				isAiming = false;
				//Debug.Log(canDash+" called by aimer of "+transform.parent.gameObject.name);
			}
		}

		if(isDashing){
			dashTimer+=Time.deltaTime;
			
			if(dashTimer<dashIn){
				//dashing in
				transform.position = Vector3.MoveTowards(transform.position,optimalSpot,(movDist-distanceFromPlayer)*Time.deltaTime/dashIn);
			}else if(dashTimer<dashIn+stayDur){
				//staying at optimal attacking position

			}else if(dashTimer<dashIn+stayDur+backOff){
				//time to back away
				transform.position = Vector3.MoveTowards(transform.position,backDashSpot, backDashMoveTime*Time.deltaTime/backOff);
				
			}else{
				//dash reset
				canDash = false;
				isDashing = false;
				dashTimer=0;
			}
		}else{
			//gradually return to parent (0.0)
			//preferrably before the next interval of attack
			//@@@@@@REQUEST WALK ANIMATION TO RELEVANT AI@@@@@@@@@@@@@@@
			transform.position = Vector3.MoveTowards(transform.position,transform.parent.position,Time.deltaTime*backOffDist);
		}
	}
	
	public void DashAtPlayer(float dashIn, float stayDur, float dashOut, float distanceFromPlayer, float dashOutDistance, bool willBackDash){
		//dashes towards the player, stays, then moves back
		//Debug.Log("Can dash has been turned on!");
		canDash = true;
		this.dashIn = dashIn;
		this.stayDur = stayDur;
		this.backOff = dashOut;
		this.distanceFromPlayer = distanceFromPlayer;
		backOffDist = dashOutDistance;
		this.willBackDash = willBackDash;
	}
	public void ResetRotation(){
		transform.rotation = Quaternion.Euler(Vector3.zero);
	}
	public void TriggerDash(){
		if(canDash){
			isDashing = true;
		}
	}

	public void AttackHasHit(){
		if(!willBackDash){
			backDashSpot = transform.parent.position;
			backDashMoveTime = movDist-distanceFromPlayer;
		}
	}
	private bool BufferedFloatEqual(float f1, float f2, float buffer){
		if(Mathf.Abs(f1-f2)<buffer){
			return true;
		}
		return false;
	}
}
