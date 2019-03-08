using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bapho_AttackHitbox : MonoBehaviour {
bool hasHit;

	private void OnTriggerEnter(Collider col){
		if(col.gameObject.CompareTag("Player")&&!hasHit){
		Debug.Log("Sustained damage!");
		hasHit = true;
		PlayerDodger_HPScript.playerDodger_HP.GetHitFor(15);
		}
	}
	public void resetHitStatus(){
		hasHit = false;
	}
}
