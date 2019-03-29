using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolMember : MonoBehaviour {
	private EnemyPool handler;
	// Use this for initialization
	public void AssignHandler(EnemyPool newHandler){
		handler = newHandler;
	}
	public void ReturnToPool(){
		handler.ReturnToPool(this);
	}
	private void OnDisable(){
		ReturnToPool();
	}
}
