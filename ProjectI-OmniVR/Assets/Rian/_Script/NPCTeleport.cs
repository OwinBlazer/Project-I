using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTeleport : MonoBehaviour {

    public NPCTrigger npcTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            npcTrigger.questActivated = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            npcTrigger.questActivated = false;
        }
    }
}
