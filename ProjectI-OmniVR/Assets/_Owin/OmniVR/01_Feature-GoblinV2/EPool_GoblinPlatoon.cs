using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EPool_GoblinPlatoon : EnemyPool {

	public void ChangePlatoonStats(float aggroLevel, int maxSpawn, float atkInterval){
		AI_GoblinPlatoon tempPlatoon;
		for(int i=0;i<transform.childCount;i++){
			tempPlatoon = transform.GetChild(i).GetComponent<AI_GoblinPlatoon>();
			tempPlatoon.SetGoblinParam(aggroLevel,atkInterval);
			tempPlatoon.SetGoblinQty(maxSpawn);
		}
	}
}
