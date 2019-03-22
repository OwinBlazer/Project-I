using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Pool : MonoBehaviour {
	[SerializeField]EnemyPool[] poolArr;
	List<PoolMember> spawnedList = new List<PoolMember>();
	public void SpawnEnemy(int id){
		GameObject tempObj = poolArr[id].IssueFromPool(transform);
		PoolMember tempMember = tempObj.GetComponent<PoolMember>();
		spawnedList.Add(tempMember);
	}

	public void DelEnemy(){
		spawnedList[0].ReturnToPool();
		spawnedList.RemoveAt(0);
	}
}
