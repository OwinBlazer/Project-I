using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_QuestReportSimulator : MonoBehaviour {

	public void ReportingDeathID(int ID){
		QuestTracker tracker =  GameObject.FindObjectOfType<QuestTracker>();
		tracker.ReportDeath(ID,0);
		//Debug.Log((tracker!=null)+"start");
	}

	public void FinishWave(){
		QuestTracker tracker =  GameObject.FindObjectOfType<QuestTracker>();
		tracker.ReportWaveEnd();
		//Debug.Log((tracker!=null)+"finish");
	}
}
