using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditAttack_Hitbox : MonoBehaviour {

	private void OnTriggerEnter(Collider col){
		if(col.gameObject.CompareTag("Player")){
			GetComponentInParent<AI_BanditAttack>().ReportHit();
			//Debug.Log("Player has been hit");
		}
	}
}
