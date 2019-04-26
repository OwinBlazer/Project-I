using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ItemDropEntry{
	public GameObject itemPrefab;
	public float baseChance;
	public float chanceGrowth;
	public int minimumWaves;
	public int maxSpawnQty;
	public int currSpawnQty;
}
[System.Serializable]
public class LootTableEntry{
	public int enemyID;
	public List<ItemDropEntry> itemDropEntry;
}
public class OW_LootSpawner : MonoBehaviour {
	public static OW_LootSpawner lootSpawner;
	private int wave;
	//[SerializeField]EnemySpawnCounter@@@@@@@@@@@@@@@@@@@@@@@@@@
	//entry index is enemy ID
	public List<LootTableEntry> LootTableEntries;
	[SerializeField]QuestTracker questTracker;
	// Use this for initialization
	void Start () {
		if(lootSpawner==null){
			lootSpawner=this;
		}else{
			Destroy(gameObject);
		}
		//wave = EnemySpawnCounter.GetWave();

		//add drops from the quests into the itemDropEntry@@@@@@@@@@@@@@@@@@@@@@@@
	}
	public void LoadQuestDrop(){
		List <QuestTrackerEntry> allActiveQuest = questTracker.GetEntryList();
		for(int i=0;i<allActiveQuest.Count;i++){
			for(int j=0;j<allActiveQuest[i].objectives.Count;j++){
				QuestObjective tempObjective = allActiveQuest[i].objectives[j];
				if(tempObjective.itemToDrop.itemPrefab!=null&&GetEnemyIndex(tempObjective.targetEnemyID)!=-1){
					OW_QuestItemDropEntry tempObjectiveDrop = tempObjective.itemToDrop;
					ItemDropEntry loadedItemEntry = new ItemDropEntry();
					loadedItemEntry.itemPrefab = tempObjectiveDrop.itemPrefab;
					loadedItemEntry.baseChance = tempObjectiveDrop.baseChance;
					loadedItemEntry.chanceGrowth = tempObjectiveDrop.growth;
					loadedItemEntry.minimumWaves = tempObjectiveDrop.minimumWaves;
					loadedItemEntry.currSpawnQty = 0;
					loadedItemEntry.maxSpawnQty = tempObjective.tartgetQuantityQuest-tempObjective.currentQuantityQuest;
					OW_LootDrop_QuestItem questItem = loadedItemEntry.itemPrefab.GetComponent<OW_LootDrop_QuestItem>();
					if(questItem!=null){
						questItem.SetQuestID(i,j);
					}
					LootTableEntries[GetEnemyIndex(tempObjective.targetEnemyID)].itemDropEntry.Add(loadedItemEntry);
				}
			}
		}
	}
	public void SpawnItemFor(OW_EnemyStats enemyStats,Transform location){
		int i=0;
		//Debug.Log("Item requested for "+enemyStats.transform.parent.parent.name);
		foreach(ItemDropEntry entry in LootTableEntries[enemyStats.GetEnemyID()].itemDropEntry){
			float RNG = Random.Range(0f,1f);
			if(RNG<entry.baseChance+entry.chanceGrowth*(wave - entry.minimumWaves)){
				//Pooling the items?@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
				if(entry.currSpawnQty<entry.maxSpawnQty){
					GameObject tempObject = Instantiate(entry.itemPrefab,location.position,Quaternion.identity);
					
					OW_LootDrop_BaseClass baseItem = tempObject.GetComponent<OW_LootDrop_BaseClass>();
					if(baseItem!=null){
						baseItem.SetSpawnID(enemyStats.GetEnemyID(),i);
					}
					entry.currSpawnQty++;
				}
			}
			i++;
		}
	}

	public void ReduceCount(int enemyID, int entryID){
		LootTableEntries[enemyID].itemDropEntry[entryID].currSpawnQty--;
	}

	private int GetEnemyIndex(int enemyID){
		for(int i=0;i<LootTableEntries.Count;i++){
			if(LootTableEntries[i].enemyID==enemyID){
				return i;
			}
		}
		return -1;
	}
}
