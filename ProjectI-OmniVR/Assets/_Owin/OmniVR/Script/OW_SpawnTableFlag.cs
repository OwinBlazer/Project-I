using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OW_SpawnTableFlag : MonoBehaviour {
    private int assignedIndex;
    private OW_SpawnTable spawnTable;
    public void AssignIndex(int index,OW_SpawnTable referenceTable)
    {
        assignedIndex = index;
        spawnTable = referenceTable;
    }
    private void OnDisable()
    {
        spawnTable.ReportInactive(assignedIndex);
    }
}
