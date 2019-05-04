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
    //if priority 0 = survival, 1 = time attack
	public int questStatus;
	public Sprite questIcon;
	public Sprite npcPortrait;
    public int goldReward;
	//questStatus 0=notTaken 1=unfinished 2=unreported 2=done
}
public class QuestTracker : MonoBehaviour {
	public static QuestTracker questTracker;
	private List<QuestTrackerEntry> questEntry = new List<QuestTrackerEntry>();
	private List<QuestTrackerEntry> waveStartQuestEntry = new List<QuestTrackerEntry>();
	private QuestTrackerEntry priorityQuest;
	[SerializeField]PlayerSO playerSav;
	[SerializeField]QuestProgressCache progressCache;
    [SerializeField]QuestTempProgress tempProgress;
    [SerializeField]QuestBoxPopulator questBoxManager;
	[SerializeField]OW_PointerGunSwitcher gunPointerSwitcher;
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
        //@@@@@@@@@@@@@@@@@THIS IS ONLY FOR DEBUG PURPOSES. LOAD AND UNLOAD BEFORE WAVE STARTS@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
		LoadQuest(GetCurrentWave());
        OW_LootSpawner lootSpawner = FindObjectOfType<OW_LootSpawner>();
        if(lootSpawner!=null){
            lootSpawner.LoadQuestDrop();
        }
	}
	
	private void LoadQuest(int wave){
		//if wave is 1, load from playersav, else draw from tempQuestProgress
        int i=0;
        if(wave==1){
            tempProgress.ResetTempProgress();
        }
		foreach(Quest q in playerSav.activQuest){
			QuestTrackerEntry tempEntry = new QuestTrackerEntry();
			tempEntry.entryName = q.npcName;
            tempEntry.objectives.Clear();
            int j=0;
            if(wave==1){
                tempProgress.questObjectiveList.Add(new ObjectiveCurrentProgress());
                tempProgress.questObjectiveList[i].currentProgress = new int[q.questObjective.Length];
            }

            int clearCount = 0;
            foreach(QuestObjective qo in q.questObjective)
            {
                QuestObjective newObjective = new QuestObjective();
                newObjective.objectiveText = qo.objectiveText;
                newObjective.targetEnemyID = qo.targetEnemyID;
                
                if(wave==1){
                    newObjective.currentQuantityQuest = qo.currentQuantityQuest;
                    tempProgress.questObjectiveList[i].currentProgress[j]=qo.currentQuantityQuest;
                }else{
                    newObjective.currentQuantityQuest = tempProgress.questObjectiveList[i].currentProgress[j];
                }
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
                newObjective.itemToDrop = qo.itemToDrop;
                newObjective.WeaponId = qo.WeaponId;
                tempEntry.objectives.Add(newObjective);
                j++;
                if(newObjective.currentQuantityQuest>=newObjective.tartgetQuantityQuest){
                    clearCount++;
                }
            }
			tempEntry.questMode = q.questMode;
			tempEntry.objectiveText = q.objective;
			tempEntry.questIcon = q.questIcon;
			tempEntry.npcPortrait = q.npcPortrait;

            if(clearCount>=q.questObjective.Length){
                tempEntry.questStatus = 2;
            }else{
			    tempEntry.questStatus = q.questStatus;
            }
            tempEntry.goldReward = q.goldReward;
			questEntry.Add(tempEntry);
            i++;
		}

		//also use to pull PRIORITY QUEST

		if(playerSav.priorityQuest!=null){
			if(playerSav.priorityQuest.questStatus==1){
				priorityQuest = new QuestTrackerEntry();
				Quest tempQuest = playerSav.priorityQuest;
				priorityQuest.entryName = tempQuest.npcName;
				priorityQuest.questMode = tempQuest.questMode;
                if(wave==1){
                    tempProgress.priorityObjectiveList.currentProgress = new int[playerSav.priorityQuest.questObjective.Length];
                }
                int j=0;
                foreach (QuestObjective qo in playerSav.priorityQuest.questObjective)
                {
                    QuestObjective newObjective = new QuestObjective();
                    newObjective.targetEnemyID = qo.targetEnemyID;
                    
                    if(wave==1){
                        newObjective.currentQuantityQuest = qo.currentQuantityQuest;
                        tempProgress.priorityObjectiveList.currentProgress[j]=qo.currentQuantityQuest;
                    }else{
                        newObjective.currentQuantityQuest = tempProgress.priorityObjectiveList.currentProgress[j];
                    }

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
                    newObjective.itemToDrop = qo.itemToDrop;
                    newObjective.WeaponId = qo.WeaponId;
                    priorityQuest.objectives.Add(newObjective);
                    j++;
                }
				priorityQuest.questStatus = tempQuest.questStatus;
            }
		}
	}

	public void ReportDeath(int EnemyID,int WeaponId){
		progressCache.ReportDeath(EnemyID,WeaponId);
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
                    ReportWaveEnd();
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
		ReportWaveEnd();
		//THIS FUNCTION IS ONLY CALLED IN THE "UNIQUE ROOM" ON THE TIMER FINISH SCRIPT@@@@@@@@@@@@@@@@@@@@@@@@@
	}
	public void ReportShieldUse(){
        //use caching to store shield usage
        progressCache.ReportShieldUse();
	}
    public void ReportQuestItemGet(int questIndex, int objectiveIndex){
        progressCache.ReportQuestItemGet(questIndex,objectiveIndex);
    }
	public void ReportWaveEnd(){

        OW_LootSpawner.lootSpawner.CollectAllDrops();
        OW_WaveSystem.waveSystem.KillAllEnemy();

        //check Kill Quest Progress via loading from cache here
        int[] progressList = progressCache.GetKilledList();
        int[] weaponUseList = progressCache.GetWeaponUseList();
        ObjectiveCache[] itemDropList = progressCache.GetQuestItemList();
        int i=0;
		foreach(QuestTrackerEntry entry in questEntry){
			if(entry.questStatus==1){
                //if fetchQuest
                if(entry.questMode==0){
                    //read into progress cache the drops
                    //add
                    int j=0;
                    foreach(QuestObjective qo in entry.objectives){
                        int progressQty = itemDropList[i].questObjectives[j];
                        if(progressQty>0){
                            qo.currentQuantityQuest+=progressQty;
                            if(qo.currentQuantityQuest>qo.tartgetQuantityQuest){
                                qo.currentQuantityQuest = qo.tartgetQuantityQuest;
                            }
                            QuestStatusCheck(entry);
                        }
                        j++;
                    }
                }else
				//if KillQuest
				if(entry.questMode==1){
                    foreach(QuestObjective qo in entry.objectives)
                    {
                        int progressQty = progressList[progressCache.GetIndexOfEnemyID(qo.targetEnemyID)];
                        if (progressQty > 0)
                        {
                            qo.currentQuantityQuest+= progressQty;
                            if(qo.currentQuantityQuest>qo.tartgetQuantityQuest){
                                qo.currentQuantityQuest = qo.tartgetQuantityQuest;
                            }
                            QuestStatusCheck(entry);
                        }
                    }

				//if weapon quest @@@@@@@@@@@@@@@@@@@@@@@@@
                //upgraded check is not active yet @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
				}else if(entry.questMode==2){
                    foreach(QuestObjective qo in entry.objectives){
                        int progressQty = weaponUseList[progressCache.GetIndexOfWeaponID(qo.WeaponId)];
                        if(progressQty>0){
                            qo.currentQuantityQuest+=progressQty;
                            if(qo.currentQuantityQuest>qo.tartgetQuantityQuest){
                                qo.currentQuantityQuest = qo.tartgetQuantityQuest;
                            }
                            //check if quest has been completed
                            QuestStatusCheck(entry);
                        }
                    }
					//(if weapon ID>0)check if entry.targetID==weapon.ID
					//check if there's an upgradable requirement
				}else //if ShieldQuest
                    if (entry.questMode==101){
				        if(entry.questStatus==1){
                            int shieldUseCount = progressCache.GetWeaponUseList()[15];
                            if(shieldUseCount>0){
                                entry.objectives[0].currentQuantityQuest +=shieldUseCount;
                                if(entry.objectives[0].currentQuantityQuest>entry.objectives[0].tartgetQuantityQuest){
                                    entry.objectives[0].currentQuantityQuest = entry.objectives[0].tartgetQuantityQuest;
                                    entry.questStatus = 2;
                                }
                            }
                        }
                    }
				}
                i++;
			}

		//also check if there's an ACTIVE PRIORITY QUEST OF TYPE TIMEATTACK
		if(priorityQuest!=null){
			if(priorityQuest.questMode==1){
				if(priorityQuest.objectives[0].currentQuantityQuest>= priorityQuest.objectives[0].tartgetQuantityQuest){
					priorityQuest.questStatus=2;
				}
			}
		}

        gunPointerSwitcher.SwitchToPointer();
        tempProgress.waveNum++;
		questBoxManager.PopulateQuestBox();
	}
	private void QuestStatusCheck(QuestTrackerEntry entry){
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
        
        for (int i = 0; i < tempProgress.questObjectiveList.Count; i++)
        {
            bool changedFlag = false;
            for (int j = 0; j < tempProgress.questObjectiveList[i].currentProgress.Length; j++)
            {
                Debug.Log("With values of quest objective length "+tempProgress.questObjectiveList[i].currentProgress.Length);
                if (tempProgress.questObjectiveList[i].currentProgress[j] < questEntry[i].objectives[j].currentQuantityQuest)
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
        //if the current target qty changes, then add to the quest tracker entry list;
        
		return updatedQuestList;
	}
	public List<QuestTrackerEntry> GetNoProgressEntryList(){
        //if the current target qty didnt change, then add to the quest tracker entry list;
        return unupdatedQuestList;
	}
    public bool GetPriorityIsUpdated(){
        if(priorityQuest.objectives.Count>0){
            bool changedFlag = false;
            for (int j = 0; j < playerSav.priorityQuest.questObjective.Length; j++)
            {
                if (playerSav.priorityQuest.questObjective[j].currentQuantityQuest < priorityQuest.objectives[j].currentQuantityQuest)
                {
                    changedFlag = true;
                }
            }
            return changedFlag;
        }
        return false;
    }

    //THIS SHOULD BE CALLED ON NEXT WAVE PRESSED@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
    public void UpdateTempQuestProgress(){
        if(priorityQuest!=null){
            //priority
            for(int i=0;i<tempProgress.priorityObjectiveList.currentProgress.Length;i++){
                tempProgress.priorityObjectiveList.currentProgress[i] = priorityQuest.objectives[i].currentQuantityQuest;
            }
        }

        //normal quests
        for(int i=0;i<tempProgress.questObjectiveList.Count;i++){
            for(int j=0;j<tempProgress.questObjectiveList[i].currentProgress.Length;j++){
                tempProgress.questObjectiveList[i].currentProgress[j] = questEntry[i].objectives[j].currentQuantityQuest;
            }
        }
    }

    //THIS SHOULD BE CALLED ON RETURN TO BAR PRESSED
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
        playerSav.gold = tempProgress.GoldCache;

        
        tempProgress.ResetTempProgress();
        tempProgress.waveNum = 1; 
	}

    public void ResetProgress(){
        tempProgress.ResetTempProgress();
    }
    private int GetCurrentWave(){
        if(tempProgress.waveNum<=1){
            tempProgress.waveNum=1;
            return 1;
        }else{
            return tempProgress.waveNum;
        }
    }
}
