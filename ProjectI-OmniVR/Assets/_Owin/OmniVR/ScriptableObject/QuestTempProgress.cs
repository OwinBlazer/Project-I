using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class QuestTempProgress : ScriptableObject {

	public List<ObjectiveCurrentProgress> questObjectiveList;
	public ObjectiveCurrentProgress priorityObjectiveList;
	public int GoldCache;
	public int waveNum;
	public void ResetTempProgress(){
		priorityObjectiveList = new ObjectiveCurrentProgress();
		questObjectiveList = new List<ObjectiveCurrentProgress>();
		GoldCache = 0;
		waveNum = 1;
	}
}

[System.Serializable]
public class ObjectiveCurrentProgress{
	public int[] currentProgress;
}