using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBoxPopulator : MonoBehaviour {
	[SerializeField]QuestBoxPool qBoxPool1;
	[SerializeField]QuestBoxPool qBoxPool2;
	[SerializeField]QuestTracker questTracker; 
	// Use this for initialization
	void Start () {
		
	}
	
	
	public void PopulateQuestBox(){
        questTracker.CalculateChangedEntry();

		//assign a QuestBox for each entry
		qBoxPool1.ResetQuestBox();

		//process priority 1st
		if(questTracker.GetPriorityQuest()!=null){
			if(questTracker.GetPriorityIsUpdated()){
				qBoxPool1.IssueQuestBox(questTracker.GetPriorityQuest(),true,true);
			}else{
				qBoxPool2.IssueQuestBox(questTracker.GetPriorityQuest(),false,true);

			}
		}
		foreach(QuestTrackerEntry entry in questTracker.GetUpdatedEntryList()){
			qBoxPool1.IssueQuestBox(entry,true,false);
			//Debug.Log("Issued");
		}
		qBoxPool2.ResetQuestBox();
		foreach(QuestTrackerEntry entry in questTracker.GetNoProgressEntryList()){
			qBoxPool2.IssueQuestBox(entry,false,false);
			//Debug.Log("Issued");
		}
		
        //CHECK IF THERE IS AN ACTIVE PRIORITY QUEST FOR ESCORT.
        //IF YES AND COMPLETE, PLAY CUTSCENE after POPULATING BOX@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //add event to the next button
    }

}
