using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AI_EnemyAttack_Melee : MonoBehaviour {
	bool isAttacking;
	bool isEngaging;
	[SerializeField]AI_PlayerFinder aiFinder;
	[SerializeField]Animator anim;
	private Transform target;

	private void Start(){
		isAttacking = false;
		isEngaging = false;
	}
	public abstract void StartAttack();
	public abstract void StopAttack();
	public bool GetIsEngaging(){
		return isEngaging;
	}
	public void SetIsEngaging(bool newVal){
		isEngaging = newVal;
	}
	public bool GetIsAttacking(){
		return isAttacking;
	}
	public void SetIsAttacking(bool newVal){
		isAttacking= newVal;
	}
	public void SetTarget(AI_PlayerFinder aiFinder){
		this.target = aiFinder.GetTarget();
	}
	public Transform GetTarget(){
		return target;
	}
	public Animator GetAnimator(){
		return anim;
	}
}
