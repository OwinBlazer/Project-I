using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OW_QuestTimerStarter : MonoBehaviour {
	[SerializeField]QuestTimer timer;
	[SerializeField]PlayerSO playerSO;
	float time;
	// Use this for initialization
	void Start () {
		timer.StartTimer(playerSO.priorityQuest.questMode,playerSO.priorityQuest.timeQuest);
	}
	void Update(){
		
		time+=Time.deltaTime;
	}
}
