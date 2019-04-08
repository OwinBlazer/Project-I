using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OW_LootDrop_QuestItem : OW_LootDrop_BaseClass
{
	int questID, objectiveID;
	[SerializeField]ParticleSystem bling;
	public void SetQuestID(int questIndex, int objectiveIndex){
		questIndex = questID;
		objectiveID = objectiveIndex;
	}
    public override void CollectItem()
    {
        QuestTracker.questTracker.ReportQuestItemGet(questID,objectiveID);
		bling.Play();
		Destroy(gameObject,bling.main.duration*3);
    }
}
