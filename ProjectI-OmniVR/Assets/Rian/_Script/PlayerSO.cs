using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerSO : ScriptableObject {

    public int gold;

    public Quest priorityQuest;

    //public Quest[] activQuest;
    public List<Quest> activQuest = new List<Quest>();
}
