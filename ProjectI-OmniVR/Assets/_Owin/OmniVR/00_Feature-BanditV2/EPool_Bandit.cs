using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EPool_Bandit : EnemyPool {
	public override GameObject IssueFromPool(Transform newPosition){
		GameObject tempObj = base.IssueFromPool(newPosition);
		tempObj.GetComponent<AI_PlayerFinder>().GetNavMeshAgent().nextPosition = newPosition.position;
		return tempObj;
	}
	public void ChangeBanditStats(float atkInterval,float startUpTime){
		AI_BanditAttack tempBandit;
		for(int i=0;i<transform.childCount;i++){
			tempBandit = transform.GetChild(i).GetComponent<AI_BanditAttack>();
			tempBandit.SetBanditParam(atkInterval,startUpTime);
		}
	}
}
