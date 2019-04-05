using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class QuestTempProgress : ScriptableObject {

	public List<ObjectiveCurrentProgress> questObjectiveList;
	public ObjectiveCurrentProgress priorityObjectiveList;
}

[System.Serializable]
public class ObjectiveCurrentProgress{
	public int[] currentProgress;
}