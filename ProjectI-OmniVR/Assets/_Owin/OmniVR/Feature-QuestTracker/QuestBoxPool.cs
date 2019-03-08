using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBoxPool : MonoBehaviour {
	public static QuestBoxPool questBoxPool;
	private List<QuestBox> questBoxList = new List<QuestBox>();
	[SerializeField]int poolSize;
	[SerializeField]GameObject QuestBoxPrefab;
	private int useCount;
	// Use this for initialization
	void Awake(){
		
		if(questBoxPool!=null){
			Destroy(gameObject);
		}else{
			questBoxPool = this;
		}
		for(int i=0;i<poolSize;i++){
				GameObject tempObj = Instantiate(QuestBoxPrefab,transform);
				tempObj.gameObject.SetActive(false);
				questBoxList.Add(tempObj.GetComponent<QuestBox>());
		}
	}
	public void ResetQuestBox(){
		foreach(QuestBox qbox in questBoxList){
			qbox.gameObject.SetActive(false);
		}
		useCount=0;
	}
	public QuestBox IssueQuestBox(QuestTrackerEntry entry){
		if(useCount<poolSize){
			QuestBox tempQuestBox = questBoxList[useCount];
			tempQuestBox.InitQuestBox(entry.entryName,entry.currentProgress,entry.targetProgress,entry.objectiveText);
			tempQuestBox.gameObject.SetActive(true);
			useCount++;
			return tempQuestBox;
		}
		return null;
	}
}
