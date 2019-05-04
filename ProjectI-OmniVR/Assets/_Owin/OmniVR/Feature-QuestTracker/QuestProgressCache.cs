using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectiveCache{
	public int[] questObjectives;
}
public class QuestProgressCache : MonoBehaviour {
	private int[] killedList = new int[50];
	private int[] weaponUseList = new int[20];
	private ObjectiveCache[] questItemList = new ObjectiveCache[20];
	public void OnEnable(){
		ResetCache();
	}

	private void ResetCache(){
		killedList = new int[50];
		weaponUseList = new int[20];

		//if weapon is upgraded, INDEX IS 20 @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
	}
	public void ReportDeath(int EnemyID, int WeaponID){
		//@@@@@@@@@@@@@@@@@@@@@@@@@@@@@^^^also add killer weapon stats
        if (WeaponID >= 0&&EnemyID>=0)
        {
            killedList[GetIndexOfEnemyID(EnemyID)]++;
            weaponUseList[GetIndexOfWeaponID(WeaponID)]++;
        }

		//if weapon has been upgraded, also add weaponUseList[20]
	}
	public void ReportQuestItemGet(int questIndex, int objectiveIndex){
		if(questItemList[questIndex]==null){
			questItemList[questIndex] = new ObjectiveCache();
		}
		if(questItemList[questIndex].questObjectives==null){
			questItemList[questIndex].questObjectives = new int[4];
		}
		questItemList[questIndex].questObjectives[objectiveIndex]++;
	}
	public void ReportShieldUse(){
		int indexOfShield = 15;
		weaponUseList[indexOfShield]++;
	}
	public int GetIndexOfEnemyID(int enemyID){
		int index = -1;
		switch(enemyID){
			//Bandit
			case 0:index = enemyID;
			break;

			//Goblin
			case 1:index = enemyID;
			break;
		}
		return index;
	}

	public int GetIndexOfWeaponID(int gunID){
		int index = -1;
		switch(gunID){

			//Handgun
			case 1: index=0;
			break;

			//shotgun
			case 2: index=1;
			break;
		}
		return index;
	}

	public int[] GetKilledList(){
		return killedList;
	}
	public int[] GetWeaponUseList(){
		return weaponUseList;
	}

	public ObjectiveCache[] GetQuestItemList(){
		return questItemList;
	}
}
