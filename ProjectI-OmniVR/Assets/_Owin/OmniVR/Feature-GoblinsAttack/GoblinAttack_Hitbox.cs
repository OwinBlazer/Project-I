using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAttack_Hitbox : MonoBehaviour {

	private void OnTriggerEnter(Collider col){
		if(col.gameObject.CompareTag("Player")){
			GetComponentInParent<GoblinSoloAI>().AttackHasHit();
			//Debug.Log("Player has been hit");
		}
	}
}
