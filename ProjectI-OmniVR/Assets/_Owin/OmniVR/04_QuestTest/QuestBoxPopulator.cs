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
		//assign a QuestBox for each entry
		qBoxPool1.ResetQuestBox();
		foreach(QuestTrackerEntry entry in questTracker.GetUpdatedEntryList()){
			qBoxPool1.IssueQuestBox(entry);
			//Debug.Log("Issued");
		}
		/*
		
		foreach(QuestTrackerEntry entry in questTracker.GetNoProgressEntryList()){
			qBoxPool2.IssueQuestBox(entry);
			//Debug.Log("Issued");
		}
		
		 */
		//CHECK IF THERE IS AN ACTIVE PRIORITY QUEST FOR ESCORT.
		//IF YES AND COMPLETE, PLAY CUTSCENE after POPULATING BOX@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
		//add event to the next button
	}

}
