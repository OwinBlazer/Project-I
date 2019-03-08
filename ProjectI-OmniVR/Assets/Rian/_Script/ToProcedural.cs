using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToProcedural : MonoBehaviour {

    public PlayerSO playerSO;

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            QuestDone();
        }
    }

    public void QuestDone()
    {
        var listing = playerSO.activQuest;
        foreach (Quest player in listing)
        {
            player.questStatus = 2;
        }
    }
}
