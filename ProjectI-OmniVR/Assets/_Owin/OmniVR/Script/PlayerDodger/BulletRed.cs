using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRed : MonoBehaviour {
	private void OnTriggerEnter(Collider col){
		if(col.gameObject.CompareTag("Player")){
			Debug.Log("Hit by red!");
		}else{
			Debug.Log("Return red!");
		}
		MatrixReloadedPool.bulletPool.ReturnBulletRed(this);
	}

}
