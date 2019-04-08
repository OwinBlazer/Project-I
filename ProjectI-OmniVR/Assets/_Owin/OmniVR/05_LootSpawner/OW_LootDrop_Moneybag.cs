using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OW_LootDrop_Moneybag : OW_LootDrop_BaseClass
{
	[SerializeField]int goldValue;
	[SerializeField]QuestTempProgress tempProgress;
	[SerializeField]ParticleSystem blingEffect;
    public override void CollectItem()
    {
		//find the correct questIndex-ObjectiveIndex for quest Items
		tempProgress.GoldCache += goldValue;
		OW_LootSpawner.lootSpawner.ReduceCount(base.GetEnemyID(),base.GetEntryID());
		blingEffect.Play();
		Destroy(gameObject,blingEffect.main.duration*3);
    }
}
