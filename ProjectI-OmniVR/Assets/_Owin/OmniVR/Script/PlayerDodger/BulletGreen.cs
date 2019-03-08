using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGreen : MonoBehaviour {

	// Use this for initialization
	private void OnTriggerEnter(Collider col){
		if(col.gameObject.CompareTag("Player")){
			Debug.Log("Hit by green!");
		}else{
			Debug.Log("Return green!");
		}
		MatrixReloadedPool.bulletPool.ReturnBulletGreen(this);
	}
}
