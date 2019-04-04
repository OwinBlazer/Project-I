using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class QuestTrackerEntry{
	public string entryName;
	public string objectiveText;
    public List<QuestObjective> objectives = new List<QuestObjective>();
	public int questMode;
	//questmode 0 = kill, 1 = fetch
	public int questStatus;
	public Sprite questIcon;
	public Sprite npcPortrait;
	//questStatus 0=notTaken 1=unfinished 2=unreported 2=done
}
public class QuestTracker : MonoBehaviour {
	public static QuestTracker questTracker;
	private List<QuestTrackerEntry> questEntry = new List<QuestTrackerEntry>();
	private QuestTrackerEntry priorityQuest;
	[SerializeField]PlayerSO playerSav;
	[SerializeField]QuestProgressCache progressCache;
    private List<QuestTrackerEntry> unupdatedQuestList = new List<QuestTrackerEntry>();
    private List<QuestTrackerEntry> updatedQuestList = new List<QuestTrackerEntry>();
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
            tempEntry.objectives.Clear();
            foreach(QuestObjective qo in q.questObjective)
            {
                QuestObjective newObjective = new QuestObjective();
                newObjective.targetEnemyID = qo.targetEnemyID;
                newObjective.currentQuantityQuest = qo.currentQuantityQuest;
                switch (q.questMode)
                {
                    case 0:
                        newObjective.tartgetQuantityQuest = 1;
                        break;
                    case 1:
                        newObjective.tartgetQuantityQuest = qo.tartgetQuantityQuest;
                        break;
                }
                newObjective.locationID = qo.locationID;
                newObjective.itemChance = qo.itemChance;
                newObjective.WeaponId = qo.WeaponId;
                tempEntry.objectives.Add(newObjective);
            }
			tempEntry.questMode = q.questMode;
			tempEntry.objectiveText = q.objective;
			tempEntry.questIcon = q.questIcon;
			tempEntry.npcPortrait = q.npcPortrait;
			tempEntry.questStatus = q.questStatus;
			questEntry.Add(tempEntry);
		}

		//also use to pull PRIORITY QUEST

		if(playerSav.priorityQuest!=null){
			if(playerSav.priorityQuest.questStatus==1){
				priorityQuest = new QuestTrackerEntry();
				Quest tempQuest = playerSav.priorityQuest;
				priorityQuest.entryName = tempQuest.npcName;
				priorityQuest.questMode = tempQuest.questMode;
                foreach (QuestObjective qo in playerSav.priorityQuest.questObjective)
                {
                    QuestObjective newObjective = new QuestObjective();
                    newObjective.targetEnemyID = qo.targetEnemyID;
                    newObjective.currentQuantityQuest = qo.currentQuantityQuest;
                    switch (playerSav.priorityQuest.questMode)
                    {
                        case 0:
                            newObjective.tartgetQuantityQuest = 1;
                            break;
                        case 1:
                            newObjective.tartgetQuantityQuest = qo.tartgetQuantityQuest;
                            break;
                    }
                    newObjective.locationID = qo.locationID;
                    newObjective.itemChance = qo.itemChance;
                    newObjective.WeaponId = qo.WeaponId;
                    priorityQuest.objectives.Add(newObjective);
                }
				priorityQuest.questStatus = tempQuest.questStatus;
            }
		}
	}

	public void ReportDeath(int EnemyID){
		progressCache.ReportDeath(EnemyID);
		if(priorityQuest!=null){
			if(priorityQuest.questMode==1)
            {
                int clearCount = 0;
                foreach (QuestObjective qo in priorityQuest.objectives)
                {
                    if (EnemyID == qo.targetEnemyID&&qo.currentQuantityQuest<qo.tartgetQuantityQuest)
                    {
                        qo.currentQuantityQuest++;
                    }
                    if (qo.tartgetQuantityQuest <= qo.currentQuantityQuest)
                    {
                        clearCount++;
                    }
                }
                if (clearCount >= priorityQuest.objectives.Count)
                {
                    //if all objectives cleared
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
				priorityQuest.objectives[0].currentQuantityQuest = priorityQuest.objectives[0].tartgetQuantityQuest;
				priorityQuest.questStatus = 2;
			}
		}
		ReportWaveEnd(1);
		//THIS FUNCTION IS ONLY CALLED IN THE "UNIQUE ROOM" ON THE TIMER FINISH SCRIPT@@@@@@@@@@@@@@@@@@@@@@@@@
	}
	public void ReportShieldUse(){
		foreach(QuestTrackerEntry entry in questEntry)
        {
            //if ShieldQuest
            if (entry.questMode==101){
				if(entry.questStatus==1){
                    int clearCount = 0;
                    foreach(QuestObjective qo in entry.objectives)
                    {
                        if (qo.currentQuantityQuest < qo.tartgetQuantityQuest)
                        {
                            qo.currentQuantityQuest++;
                        }
                        else
                        {
                            clearCount++;
                        }
                    }
					if(clearCount>=entry.objectives.Count){
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
					if(WaveNum>entry.objectives[0].tartgetQuantityQuest){
						entry.objectives[0].currentQuantityQuest=1;
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
                    foreach(QuestObjective qo in entry.objectives)
                    {
                        int progressQty = progressList[progressCache.GetIndexOfEnemyID(qo.targetEnemyID)];
                        if (progressQty > 0)
                        {
                            qo.currentQuantityQuest+= progressQty;
                            KillQuestStatusCheck(entry);
                        }
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
				if(priorityQuest.objectives[0].currentQuantityQuest>= priorityQuest.objectives[0].tartgetQuantityQuest){
					priorityQuest.questStatus=2;
				}
			}
		}

		//PopulateQuestBox();
	}
	private void KillQuestStatusCheck(QuestTrackerEntry entry){
        //check if it is killquest or fetchquest
        int clearCount = 0;
        foreach(QuestObjective qo in entry.objectives)
        {
            if (qo.tartgetQuantityQuest <= qo.currentQuantityQuest)
            {
                clearCount++;
            }
        }
        if (clearCount >= entry.objectives.Count)
        {
            entry.questStatus = 2;
        }
	}
    /*
	public string GetNameOf(int EnemyID){
		//@@@REFER TO AN ENEMY DATABASE OUTSIDE*******
		return "";
	}
	 */
    public List<QuestTrackerEntry> GetEntryList()
    {
        return questEntry;
    }
	public QuestTrackerEntry GetPriorityQuest(){
		return priorityQuest;
	}
    public void CalculateChangedEntry()
    {
        updatedQuestList.Clear();
        unupdatedQuestList.Clear();
        for (int i = 0; i < playerSav.activQuest.Count; i++)
        {
            bool changedFlag = false;
            for (int j = 0; j < playerSav.activQuest[i].questObjective.Length; j++)
            {
                if (playerSav.activQuest[i].questObjective[j].currentQuantityQuest < questEntry[i].objectives[j].currentQuantityQuest)
                {
                    changedFlag = true;
                }
            }
            if (changedFlag)
            {
                updatedQuestList.Add(questEntry[i]);
            }
            else
            {
                unupdatedQuestList.Add(questEntry[i]);
            }
        }
    }
	public List<QuestTrackerEntry> GetUpdatedEntryList(){
        //still in debug@@@@@@@@@@@@@@@@@@@@@@@@@
        //read from playersav, compare to current list
        //if the current target qty changes, then add to the quest tracker entry list;
        
		return updatedQuestList;
	}
	public List<QuestTrackerEntry> GetNoProgressEntryList(){
        //still in debug@@@@@@@@@@@@@@@@@@@@@@@@@
        //if the current target qty didnt change, then add to the quest tracker entry list;
        return unupdatedQuestList;
	}
	public void SaveQuestProgress(){
		//RETURN DATA TO playerSav
		for(int i=0;i<playerSav.activQuest.Count;i++){
            for(int j = 0; j < questEntry[i].objectives.Count; j++)
            {
                playerSav.activQuest[i].questObjective[j].currentQuantityQuest = questEntry[i].objectives[j].currentQuantityQuest;
            }
			playerSav.activQuest[i].questStatus = questEntry[i].questStatus;
		}
        if (priorityQuest != null)
        {
            playerSav.priorityQuest.questObjective = priorityQuest.objectives.ToArray();
            playerSav.priorityQuest.questStatus = priorityQuest.questStatus;
        }
	}
}
