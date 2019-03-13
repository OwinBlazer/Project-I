using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BanditAttackTrigger : MonoBehaviour {
	[SerializeField]private BanditSoloAI banditAI;
	[SerializeField] NavMeshAgent navAgent;
	[SerializeField] Transform playerTransform;
	private void OnTriggerEnter(Collider col){
		if(col.gameObject.CompareTag("Player")){
			navAgent.isStopped = true;
			banditAI.StartAttacking();
		}
	}
	private void Start(){
		//debug@@@@@@
			StartMovementTo(playerTransform.position);
		//debug-------------
	}
	public void StartMovementTo(Vector3 target){
		navAgent.SetDestination(target);
		navAgent.isStopped = false;
	}
}
