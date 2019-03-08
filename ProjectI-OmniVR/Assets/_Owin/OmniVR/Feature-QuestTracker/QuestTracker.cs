using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class QuestTrackerEntry{
	public int targetID;
	public string entryName;
	public string objectiveText;
	public int currentProgress;
	public int targetProgress;
	public int questMode;
	//questmode 0 = kill, 1 = fetch
	public int questStatus;
	//questStatus 0=notTaken 1=unfinished 2=unreported 2=done
}
public class QuestTracker : MonoBehaviour {
	public static QuestTracker questTracker;
	private List<QuestTrackerEntry> QuestEntry = new List<QuestTrackerEntry>();
	[SerializeField]QuestBoxPool qBoxPool;
	[SerializeField]PlayerSO playerSav;
	private void Awake(){
		if(questTracker!=null){
			Destroy(gameObject);
		}else{
			questTracker = this;
		}
	}
	// Use this for initialization
	void Start () {
		LoadQuest();
	}
	
	private void LoadQuest(){
		//Pulled Quests into QuestEntry list, one by one @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
		//PlayerSO.activQuest
		foreach(Quest q in playerSav.activQuest){
			QuestTrackerEntry tempEntry = new QuestTrackerEntry();
			tempEntry.entryName = q.npcName;
			tempEntry.targetID = q.targetEnemyID;
			tempEntry.currentProgress = 0;
			switch(q.questMode){
				case 0:	tempEntry.targetProgress = q.tartgetQuantityQuest;
				break;
				case 1:tempEntry.targetProgress = 1;
				break;
			}
			tempEntry.currentProgress = q.currentQuantityQuest;
			tempEntry.questMode = 0;
			tempEntry.objectiveText = q.objective;
			QuestEntry.Add(tempEntry);
		}
	}

	public void ReportDeath(int EnemyID){
		//for use when enemy dies
		foreach(QuestTrackerEntry entry in QuestEntry){
			if(entry.questMode==0 && entry.questStatus==1&&entry.targetID==EnemyID){
				entry.currentProgress++;
				KillQuestStatusCheck(entry);
			}
		}
	}
	public void ReportWaveEnd(int WaveNum){
		//for use when wave ends, to display quest and to check key item drop
		foreach(QuestTrackerEntry entry in QuestEntry){
			if(entry.questMode==1 && entry.questStatus==1&&WaveNum>=entry.targetProgress){
				entry.currentProgress=1;
				entry.questStatus = 2;
			}
		}
		PopulateQuestBox();
	}
	private void KillQuestStatusCheck(QuestTrackerEntry entry){
		//check if it is killquest or fetchquest
		if(entry.currentProgress>=entry.targetProgress){
				entry.questStatus = 2;
		}
	}
	public string GetNameOf(int EnemyID){
		//@@@REFER TO AN ENEMY DATABASE OUTSIDE*******
		return "";
	}
	public void PopulateQuestBox(){
		//assign a QuestBox for each entry
		qBoxPool.ResetQuestBox();
		foreach(QuestTrackerEntry entry in QuestEntry){
			qBoxPool.IssueQuestBox(entry);
			//Debug.Log("Issued");
		}
	}
	public void SaveQuestProgress(){
		//RETURN DATA TO playerSav
		List<Quest> tempQuestStatus = new List<Quest>();
		foreach(QuestTrackerEntry entry in QuestEntry){
			Quest tempQuest = new Quest();
			//set the quest data here@@@@@@@@@@@@@@@@@@@@@@@@@@@@
			tempQuestStatus.Add(tempQuest);
		}
		playerSav.activQuest = tempQuestStatus;
	}
}
