using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System. Serializable]
class DirectionLane{
	public Transform[] spawnLane;
}
public class OW_WaveSystem : MonoBehaviour {
	[SerializeField]int waveEndType;
	// 0 := Time Based
	// 1 := Spawn Based
	[SerializeField]float baseEndTime;
	[SerializeField]int spawnEndQty;
	[SerializeField]float timeDirectionMin;
	[SerializeField]float timeDirectionMax;
	private float timeDirectionCurr;
	private int chosenDirection;
	[SerializeField]float timeSpawnIntervalMin;
	[SerializeField]float timeSpawnIntervalMax;
	private float timeSpawnIntervalCurr;
	[SerializeField]DirectionLane[] spawnPoint;
	[SerializeField]OW_SpawnTable spawnTable;

	private float startTime;
	private int spawnNum;
	private bool spawnEnd;
	// Use this for initialization
	void Start () {
		Initialize();
	}
	private void Initialize(){
		timeDirectionCurr = GetDirectionTime();
		timeSpawnIntervalCurr = GetSpawnTime();
		//randomize chosen direction
		chosenDirection = GetNewDirection();
		startTime = Time.time;
		spawnNum = 0;
		spawnEnd = false;
	}
	// Update is called once per frame
	void Update () {
		if(!spawnEnd){
			//direction roll
			if(timeDirectionCurr>0){
				timeDirectionCurr-=Time.deltaTime;
			}else{
				//roll for which direction
				timeDirectionCurr = GetDirectionTime();
			}

			//spawn roll
			if(timeSpawnIntervalCurr>0){
				timeSpawnIntervalCurr-=Time.deltaTime;
			}else{
				GameObject tempObject=null;
				int loopNum=0;
				while(tempObject==null&&loopNum<5){
					tempObject = spawnTable.SpawnEnemyRandom(spawnPoint[chosenDirection].spawnLane[GetSpawnLane()]);
				}
				if(tempObject!=null){
					spawnNum++;
				}
				timeSpawnIntervalCurr = GetSpawnTime();
			}

			//check for wave end
			switch(waveEndType){
				case 0:
					if((Time.time-startTime)>GetEndTime()){
						spawnEnd = true;
					}
				break;
				case 1:
					if(spawnNum>= spawnEndQty){
						spawnEnd = true;
					}
				break;
			}
		}else{
			if(spawnTable.GetActiveNum()<=0){
				//trigger wave end here@@@@@@@@@
				Debug.Log("Wave END!!");
				this.enabled = false;
				Debug.Break();
			}
		}
	}

	private float GetDirectionTime(){
		return Random.Range(timeDirectionMin,timeDirectionMax);
	}

	private float GetSpawnTime(){
		return Random.Range(timeSpawnIntervalMin,timeSpawnIntervalMax);
	}
	private int GetNewDirection(){
		return Random.Range(0,spawnPoint.Length);
	}
	private int GetSpawnLane(){
		return Random.Range(0,spawnPoint[chosenDirection].spawnLane.Length);
	}

	private float GetEndTime(){
		//scalable to wave Num? @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
		return baseEndTime;
	}
}
