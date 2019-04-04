using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBoxDisplayManager : MonoBehaviour {
    [SerializeField] GameObject UpdatedQuestGroup;
    [SerializeField] GameObject UnchangedQuestGroup;
    // Use this for initialization
    public void SwitchToUnchanged()
    {
        UnchangedQuestGroup.SetActive(true);
        UpdatedQuestGroup.SetActive(false);
    }

    public void SwitchToChanged()
    {
        UnchangedQuestGroup.SetActive(false);
        UpdatedQuestGroup.SetActive(true);
    }
}
