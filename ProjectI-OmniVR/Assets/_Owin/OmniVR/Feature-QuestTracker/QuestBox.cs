using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestBox : MonoBehaviour {
	[SerializeField]private Text QuestName;
	[SerializeField]private Text QuestCounter;
	[SerializeField]private Text QuestMax;
	[SerializeField]private Text QuestObjective;
	// Use this for initialization
	public void InitQuestBox(string name, int count, int max, string objective){
		QuestName.text = name;
		QuestCounter.text = count.ToString();
		QuestMax.text = max.ToString();
		QuestObjective.text = objective;
	}
	public void SetCounter(int count){
		QuestCounter.text = count.ToString();
	}
}
