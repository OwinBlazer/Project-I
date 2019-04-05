using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBoxDisplayManager : MonoBehaviour {
    [SerializeField] GameObject UpdatedQuestGroup;
    [SerializeField] GameObject UnchangedQuestGroup;
    [SerializeField] QuestTracker questTracker;
    [SerializeField]GameObject SwitchToUpdatedButton;
    // Use this for initialization
    public void Start(){
        UnchangedQuestGroup.SetActive(false);
    }
    public void InitializeBox(){
        if(questTracker.GetUpdatedEntryList().Count>0){
            SwitchToChanged();
            SwitchToUpdatedButton.SetActive(true);
        }else{
            SwitchToUnchanged();
            SwitchToUpdatedButton.SetActive(false);
        }
    }
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
