using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Quest : ScriptableObject
{

    public int questID;
    public string npcName;

    [TextArea(3, 10)]
    public string[] questDialogue;

    [TextArea(3, 10)]
    public string defaultDialogue;

    [TextArea(3, 10)]
    public string[] rewardDialogue;

    [TextArea(3, 10)]
    public string confirmDialogue;

    [TextArea(3, 10)]
    public string declineDialogue;

    [TextArea(3, 10)]
    public string objective;

    public int goldReward;

    public int questStatus; // this for know how quest progress. 0 : available, 1: Onprogress , 2 : Finished 3 : Confirmed

    public int targetEnemyID;//targetid for enemy target 0 : Bandit, 1 : Goblin

    public int questMode;//questMode 0 : kill, 1 :Key , 2 = Chance, 3 : Survival Random, 4 : Survival Specified Target, 5:Time Attack Random, 6:Time Attack Specified;

    public int currentQuantityQuest;//currentquantityquest

    public int tartgetQuantityQuest;//target Quantity Quest

    public int locationID;// 0: allLocation, 1 : Forest, 2 : Cave

    public int timeQuest; //

    public int itemChance;//

}
