using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System. Serializable]
class DirectionLane{
	public Transform[] spawnLane;
}
public class OW_WaveSystem : MonoBehaviour {
	[Tooltip("0 ends wave when time limit is reached\n1 ends wave when spawn limit is reached\n2 ends wave if EITHER spawn limit or time limit is reached\n3 ends wave if BOTH spawn limit and time limit are reached")][SerializeField][Range(0,3)]int waveEndType;
	// 0 := Time Based
	// 1 := Spawn Based
	// 2 := Time OR Spawn has reached limit
	// 3 := Time AND Spawn has reached limit
	[Tooltip("Time limit of when the wave will end, adjustable to wave number if necessary")][SerializeField]float baseEndTime;
	[Tooltip("Spawn limit of when the wave will end")][SerializeField]int spawnEndQty;
	[Tooltip("Minimum time of when the direction of spawning will change")][SerializeField]float timeDirectionMin;
	[Tooltip("Maximum time of when the direction of spawning will change")][SerializeField]float timeDirectionMax;
	private float timeDirectionCurr;
	private int chosenDirection;
	[Tooltip("Minimum time of when the next enemy will be spawned")][SerializeField]float timeSpawnIntervalMin;
	[Tooltip("Maximum time of when the next enemy will be spawned")][SerializeField]float timeSpawnIntervalMax;
	private float timeSpawnIntervalCurr;
	[Tooltip("Every spawn point, divided by each direction possible")][SerializeField]DirectionLane[] spawnPoint;
	[SerializeField]OW_SpawnTable spawnTable;
	[SerializeField]QuestTracker questTracker;

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
					loopNum++;
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
				case 2:
					if(spawnNum>= spawnEndQty||(Time.time-startTime)>GetEndTime()){
						spawnEnd = true;
					}
				break;
				case 13:
					if(spawnNum>= spawnEndQty&&(Time.time-startTime)>GetEndTime()){
						spawnEnd = true;
					}
				break;
			}
		}else{
			//@@@@@@@@@@@@@@@@@@@UPON DEATH FOR PRIORITY TIME LIMIT, USE SPECIAL SCRIPT TO CAUSE REMAINING ENEMIES TO DIE/RUN AWAY(SO SPAWNTABLE ACTIVE NUM IS NOW 0)@@@@@@@@@@@@
			//?????????????(spawn running enemies on the location of living ones?)??????????????
			if(spawnTable.GetActiveNum()<=0){
				//trigger wave end here
				//start the report screen pop up
				questTracker.ReportWaveEnd();

				this.enabled = false;
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
		Debug.Log(spawnPoint.Length);
		return Random.Range(0,spawnPoint.Length);
	}
	private int GetSpawnLane(){
		return Random.Range(0,spawnPoint[chosenDirection].spawnLane.Length);
	}

	private float GetEndTime(){
		//scalable to wave Num? @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
		//formula? Based on each wave?
		return baseEndTime;
	}

}
