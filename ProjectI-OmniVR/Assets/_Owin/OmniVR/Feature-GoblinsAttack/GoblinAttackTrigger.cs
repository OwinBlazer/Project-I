using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoblinAttackTrigger : MonoBehaviour {
	[SerializeField] GoblinPlatoonHandler platoonHandler;
	[SerializeField] NavMeshAgent navAgent;
	[SerializeField] Transform playerTransform;
	private void Start(){
		//debug@@@@@@
			StartMovementTo(playerTransform.position);
		//debug-------------
	}
	public void StartMovementTo(Vector3 target){
		navAgent.SetDestination(target);
		navAgent.isStopped = false;
	}
	private void OnTriggerEnter(Collider col){
		if(col.gameObject.CompareTag("Player")){
			platoonHandler.StartAttacking();
			navAgent.isStopped = true;
		}
	}
}
