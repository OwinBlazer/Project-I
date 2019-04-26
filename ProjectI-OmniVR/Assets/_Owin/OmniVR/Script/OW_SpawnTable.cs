using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SpawnEntry {
    public EnemyPool enemyPool;
    public float spawnChance;
    public int spawnLimit;
    public int spawnCurr;
}

public class OW_SpawnTable : MonoBehaviour {
    private int activeCount;
    [SerializeField] SpawnEntry[] spawnList;
	// Use this for initialization
	void Start () {
		activeCount = 0;
	}

    public GameObject SpawnEnemyRandom(Transform position)
    {
        float total = 0;
        foreach(SpawnEntry entry in spawnList)
        {
            total += entry.spawnChance;
        }

        float rng = Random.Range(0, total);

        int i = 0;
        while (rng > spawnList[i].spawnChance)
        {
            rng -= spawnList[i].spawnChance;
            i++;
        }
        GameObject tempObject;
        if (spawnList[i].spawnLimit > 0)
        {
            if (spawnList[i].spawnCurr < spawnList[i].spawnLimit)
            {
                activeCount++;
                spawnList[i].spawnCurr++;
                tempObject = spawnList[i].enemyPool.IssueFromPool(position);
                if(tempObject!=null){
                    tempObject.GetComponent<OW_SpawnTableFlag>().AssignIndex(i, this);
                }
                return tempObject;
            }
            else
            {
                return null;
            }
        }
        else
        {
            activeCount++;
            tempObject = spawnList[i].enemyPool.IssueFromPool(position);
            if(tempObject!=null){
                tempObject.GetComponent<OW_SpawnTableFlag>().AssignIndex(i, this);
            }
            return tempObject;
        }
    }
    public GameObject SpawnEnemyType(int ID, Transform position)
    {
        int index = -1;
        for(int i = 0; i < spawnList.Length; i++)
        {
            if (spawnList[i].enemyPool.GetID() == ID)
            {
                index = i;
                break;
            }
        }
        if (index != -1)
        {
            if (spawnList[index].spawnLimit > 0)
            {
                if(spawnList[index].spawnCurr<spawnList[index].spawnLimit){
                    activeCount++;
                    spawnList[index].spawnCurr++;
                    GameObject tempObject = spawnList[index].enemyPool.IssueFromPool(position);
                    if(tempObject!=null){
                        tempObject.GetComponent<OW_SpawnTableFlag>().AssignIndex(index, this);
                    }
                    return tempObject;
                }else{
                    return null;
                }
            }
            else
            {
                activeCount++;
                GameObject tempObject = spawnList[index].enemyPool.IssueFromPool(position);
                if(tempObject!=null){
                    tempObject.GetComponent<OW_SpawnTableFlag>().AssignIndex(index, this);
                }
                return tempObject;
            }
        }
        else
        {
            return null;
        }
    }
    public void ReportInactive(int index)
    {
        spawnList[index].spawnCurr--;
        activeCount--;
    }
    public int GetActiveNum(){
        return activeCount;
    }

    public SpawnEntry[] GetSpawnList(){
        return spawnList;
    }
}
