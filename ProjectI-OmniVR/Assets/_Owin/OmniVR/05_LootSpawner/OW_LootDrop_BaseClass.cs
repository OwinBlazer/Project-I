using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OW_LootDrop_BaseClass : MonoBehaviour {
	private int enemyID, entryID;
	private bool isShot;
	private void OnCollisionEnter(Collision col){
		if(!isShot){
			if(col.gameObject.CompareTag("Bullet")){
				CollectItem();
				isShot = true;
			}
		}
		
	}
	private void OnEnable(){
		isShot = false;
	}
	public void SetSpawnID(int enemyID, int entryID){
		this.enemyID = enemyID;
		this.entryID = entryID;
	}
	
	public int GetEnemyID(){
		return enemyID;
	}
	public int GetEntryID(){
		return entryID;
	}
	public abstract  void CollectItem();
}
