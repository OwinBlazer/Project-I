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
	[SerializeField]QuestProgressCache progressCache;
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
			tempEntry.questMode = q.questMode;
			tempEntry.objectiveText = q.objective;
			QuestEntry.Add(tempEntry);
		}

		//also use to pull PRIORITY QUEST@@@@@@@@@@@@@@@@@@@@@@@@
		
	}

	public void ReportDeath(int EnemyID){
		progressCache.ReportDeath(EnemyID);
	}

	public void ReportSurvival(){
		//VALIDATE ACTIVE PRIORITY QUEST FOR TYPE 0
		//THIS FUNCTION IS ONLY CALLED IN THE "UNIQUE ROOM"@@@@@@@@@@@@@@@@@@@@@@@@@
	}
	public void ReportShieldUse(){
		foreach(QuestTrackerEntry entry in QuestEntry){
			if(entry.questStatus==1){
				//if ShieldQuest
				if(entry.questMode==101){
					entry.currentProgress++;
					if(entry.currentProgress>=entry.targetProgress){
						entry.questStatus = 2;
					}
				}
			}
		}
	}
	public void ReportWaveEnd(int WaveNum){
		//for use when wave ends, to display quest and to check key item drop
		foreach(QuestTrackerEntry entry in QuestEntry){
			if(entry.questStatus==1){
				//if WaveQuest
				if(entry.questMode==0){
					if(WaveNum>entry.targetProgress){
						entry.currentProgress=1;
						entry.questStatus = 2;
					}
				}
			}
		}
		//check Kill Quest Progress via loading from cache here
		int[] progressList = progressCache.GetKilledList();
		foreach(QuestTrackerEntry entry in QuestEntry){
			if(entry.questStatus==1){
				//if KillQuest
				if(entry.questMode==1){
					int progressQty = progressList[progressCache.GetIndexOfEnemyID(entry.targetID)];
					if( progressQty>0){
						entry.currentProgress+=progressQty;
						KillQuestStatusCheck(entry);
					}

				//if weapon quest @@@@@@@@@@@@@@@@@@@@@@@@@
				}else if(entry.questMode==2){
					//(if weapon ID>0)check if entry.targetID==weapon.ID
					//check if there's an upgradable requirement
				}
			}
		}

		//also check if there's an ACTIVE PRIORITY QUEST OF TYPE TIMEATTACK @@@@@@@@@@@@@@@@@@@@@@@@

		//CHECK IF THERE IS AN ACTIVE PRIORITY QUEST FOR ESCORT.
		//IF YES AND COMPLETE, PLAY CUTSCENE BEFORE POPULATING BOX@@@@@@@@@@@@@@@
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
		//if PRIORITY QUEST exists, issue PRIORITY QUEST 1ST@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

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
			//adapt according to entry.questMode
			//tempQuestStatus.qty = entry.qty
			//^^etc.
			tempQuestStatus.Add(tempQuest);
		}
		playerSav.activQuest = tempQuestStatus;
	}
}
