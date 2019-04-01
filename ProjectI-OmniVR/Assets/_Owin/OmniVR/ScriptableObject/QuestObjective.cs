using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestObjective {
    public int targetEnemyID;//targetid for enemy target 0 : Bandit, 1 : Goblin

   public int currentQuantityQuest;//currentquantityquest

    public int tartgetQuantityQuest;//target Quantity Quest

    public int locationID;// 0: allLocation, 1 : Forest, 2 : Cave

    public int itemChance;//

    public int WeaponId;//0 : all Weapon, 1: handGund, 2:ShootGun, 3 : SMG, 4 : Javelin, 5 : Shield, 101: All Upgraded Weapon
}
