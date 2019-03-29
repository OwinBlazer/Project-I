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
	private List<QuestTrackerEntry> questEntry = new List<QuestTrackerEntry>();
	private QuestTrackerEntry priorityQuest;
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
		//PlayerSO.activQuest
		foreach(Quest q in playerSav.activQuest){
			QuestTrackerEntry tempEntry = new QuestTrackerEntry();
			tempEntry.entryName = q.npcName;
			tempEntry.targetID = q.targetEnemyID;
			tempEntry.currentProgress = q.currentQuantityQuest;
			switch(q.questMode){
				case 0:	tempEntry.targetProgress = q.tartgetQuantityQuest;
				break;
				case 1:tempEntry.targetProgress = 1;
				break;
			}
			tempEntry.questMode = q.questMode;
			tempEntry.objectiveText = q.objective;
			questEntry.Add(tempEntry);
		}

		//also use to pull PRIORITY QUEST@@@@@@@@@@@@@@@@@@@@@@@@
		if(playerSav.priorityQuest!=null){
			if(playerSav.priorityQuest.questStatus==1){
				priorityQuest = new QuestTrackerEntry();
				Quest tempQuest = playerSav.priorityQuest;
				priorityQuest.entryName = tempQuest.npcName;
				priorityQuest.questMode = tempQuest.questMode;
				priorityQuest.currentProgress = tempQuest.currentQuantityQuest;
				switch(priorityQuest.questMode){
					case 0:priorityQuest.targetProgress = tempQuest.timeQuest;
					break;
					case 1:priorityQuest.targetProgress = tempQuest.tartgetQuantityQuest;
					break;
					case 2:priorityQuest.targetProgress = tempQuest.tartgetQuantityQuest;
					break;
				}
			}
		}
	}

	public void ReportDeath(int EnemyID){
		progressCache.ReportDeath(EnemyID);
		if(priorityQuest!=null){
			if(priorityQuest.questMode==1){
				priorityQuest.currentProgress++;
				if(priorityQuest.currentProgress>=priorityQuest.targetProgress){
					FindObjectOfType<QuestTimer>().StopTimer();
					ReportWaveEnd(1);
				}
			}
		}
	}

	public void ReportSurvival(){
		//VALIDATE ACTIVE PRIORITY QUEST FOR TYPE 0
		if(priorityQuest!=null){
			if(priorityQuest.questMode==0){
				priorityQuest.currentProgress = priorityQuest.targetProgress;
				priorityQuest.questStatus = 2;
			}
		}
		ReportWaveEnd(1);
		//THIS FUNCTION IS ONLY CALLED IN THE "UNIQUE ROOM" ON THE TIMER FINISH SCRIPT@@@@@@@@@@@@@@@@@@@@@@@@@
	}
	public void ReportShieldUse(){
		foreach(QuestTrackerEntry entry in questEntry){
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
		foreach(QuestTrackerEntry entry in questEntry){
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
		foreach(QuestTrackerEntry entry in questEntry){
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
		if(priorityQuest!=null){
			if(priorityQuest.questMode==1){
				if(priorityQuest.currentProgress>=priorityQuest.targetProgress){
					priorityQuest.questStatus=2;
				}
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
	/*
	public string GetNameOf(int EnemyID){
		//@@@REFER TO AN ENEMY DATABASE OUTSIDE*******
		return "";
	}
	 */
	public void PopulateQuestBox(){
		//assign a QuestBox for each entry
		qBoxPool.ResetQuestBox();
		//if PRIORITY QUEST exists, issue PRIORITY QUEST 1ST\
		if(priorityQuest!=null){
			qBoxPool.IssueQuestBox(priorityQuest);
		}
		foreach(QuestTrackerEntry entry in questEntry){
			qBoxPool.IssueQuestBox(entry);
			//Debug.Log("Issued");
		}
		
		//CHECK IF THERE IS AN ACTIVE PRIORITY QUEST FOR ESCORT.
		//IF YES AND COMPLETE, PLAY CUTSCENE after POPULATING BOX@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
		//add event to the next button
	}
	public void SaveQuestProgress(){
		//RETURN DATA TO playerSav
		for(int i=0;i<playerSav.activQuest.Count;i++){
			playerSav.activQuest[i].currentQuantityQuest = questEntry[i].currentProgress;
			playerSav.activQuest[i].questStatus = questEntry[i].questStatus;
		}
	}
}
