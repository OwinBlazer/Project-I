using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
class ObjectiveBox {
    private static string slash = "/";
    [SerializeField]private Text objTitle;
    [SerializeField] private Text objCount;
    [SerializeField] private GameObject gameobject;
    public void DisableObj()
    {
        gameobject.SetActive(false);
    }
    public void InitializeObjBox(string title, int currCount, int maxCount)
    {
        objTitle.text = title;
        objCount.text = currCount + slash + maxCount;
        gameobject.SetActive(true);
    }
}

public class QuestBox : MonoBehaviour {
	[SerializeField]private Text QuestName;
    [SerializeField] private ObjectiveBox[] objBox;
	[SerializeField]private Text QuestObjective;
    [SerializeField] private Image NPCPortrait;
    [SerializeField] private Image QuestIcon;

	// Use this for initialization
	public void InitQuestBox(string name, string objective, Sprite npcSprite, Sprite questIcon, QuestObjective[] objList){
		QuestName.text = name;
        foreach(ObjectiveBox ob in objBox)
        {
            ob.DisableObj();
        }
        for(int i = 0; i < objList.Length; i++)
        {
            objBox[i].InitializeObjBox(name, objList[i].currentQuantityQuest, objList[i].tartgetQuantityQuest);
        }
		QuestObjective.text = objective;
        NPCPortrait.sprite = npcSprite;
        QuestIcon.sprite = questIcon;
        gameObject.SetActive(true);
	}
}
