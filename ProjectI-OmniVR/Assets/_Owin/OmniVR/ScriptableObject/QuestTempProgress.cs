using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class QuestTempProgress : ScriptableObject {

	public List<ObjectiveCurrentProgress> questObjectiveList;
	public ObjectiveCurrentProgress priorityObjectiveList;
	public int GoldCache;

	public void ResetTempProgress(){
		questObjectiveList = new List<ObjectiveCurrentProgress>();
		GoldCache = 0;
	}
}

[System.Serializable]
public class ObjectiveCurrentProgress{
	public int[] currentProgress;
}