using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OW_LootDrop_Despawner : MonoBehaviour {
	[SerializeField]float despawnTime;
	[SerializeField]OW_LootDrop_BaseClass baseClass;
	private float currTime=0;
	private bool isShot;
	void OnEnable(){
		isShot = false;
	}
	// Update is called once per frame
	void Update () {
		if(!isShot){
			if(currTime<despawnTime){
				currTime +=Time.deltaTime;
			}else{
				currTime = 0;
				OW_LootSpawner.lootSpawner.ReduceCount(baseClass.GetEnemyID(),baseClass.GetEntryID());
				Destroy(gameObject);
			}
		}
	}
	void OnCollisionEnter(Collision col){
		if(col.gameObject.CompareTag("Bullet")){
			isShot = true;
		}
	}
}
