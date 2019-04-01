﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OW_DungeonSelector : MonoBehaviour {
    [SerializeField] Scene combatScene;
    [SerializeField] PlayerSO playerSav;
	// Use this for initialization
	public void checkQuestDestination()
    {
        Quest tempQuest = playerSav.priorityQuest;
        if (tempQuest != null)
        {
            if (tempQuest.sceneToLoad!=null)
            {
                SceneManager.LoadScene(tempQuest.sceneToLoad);
            }
            else
            {
                SceneManager.LoadScene(combatScene.name);
            }
        }
        else
        {
            SceneManager.LoadScene(combatScene.name);
        }
    }
}
