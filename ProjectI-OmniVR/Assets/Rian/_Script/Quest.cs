using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class Quest : ScriptableObject
{
    public bool priority;

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

    public int questMode;//questMode 0 : kill, 1 :Key , 2 = Chance, 3 : Survival Random, 4 : Survival Specified Target, 5:Time Attack Random, 6:Time Attack Specified;

    public QuestObjective[] questObjective;

    public int timeQuest; //

    public string sceneToLoad; //null goes to procedural scene, else goes to that specific scene
}
