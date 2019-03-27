using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    public int area; //kode area dalam bentuk int mulai dr 0
    public GameObject[] enemies;
    public Transform[] spawnpoints;
    public int maxEnemyAmount;
    public int curEnemyAmount;
    public float delayNextSpawn;
    public int enemyAlive;
    public bool spawnDone = true;
    public bool waveDone;
    GameObject enemyType;
    GameObject instanceEnemy;

    // Use this for initialization
    void Start()
    {
        //maxEnemyAmount = Random.Range(1, 4);          
    }

    // Update is called once per frame
    void Update()
    {
        if (curEnemyAmount < maxEnemyAmount)
        {
            if (spawnDone)
            {
                spawnDone = false;
                EnemyPercentageByArea();
                StartCoroutine(SpawnWave(enemyType));
            }
        }
        else
        {
            if (enemyAlive <= 0)
            {
                waveDone = true;
            }
        }
    }

    void EnemyPercentageByArea()
    {
        int rand = Random.Range(0, 100);

        if (area == 0)
        {
            if (rand <= 70)
            {
                enemyType = enemies[0];
            }
            else if (rand > 70 && rand <= 100)
            {
                enemyType = enemies[1];
            }
        }
        else
        if (area == 1)
        {
            if (rand <= 70)
            {
                enemyType = enemies[1];
            }
            else if (rand > 70 && rand <= 100)
            {
                enemyType = enemies[0];
            }
        }
        else
        if (area == 2)
        {
            //if (rand <= 50)
            //{
            //    enemyType = enemies[2];
            //}
            //else if (rand > 50 && rand <= 75)
            //{
            //    enemyType = enemies[0];
            //}
            //else if (rand > 75 && rand <= 100)
            //{
            //    enemyType = enemies[1];
            //}
        }
    }

    IEnumerator SpawnWave(GameObject _enemy)
    {
        Transform spawnpoint = spawnpoints[Random.Range(0, spawnpoints.Length)];
        instanceEnemy = Instantiate(_enemy, spawnpoint.position, spawnpoint.rotation);
        //SpawnFromPool();

        //instanceEnemy.transform.position = spawnpoint.transform.position;
        //instanceEnemy.transform.rotation = spawnpoint.transform.rotation;
        
        curEnemyAmount++;
        enemyAlive++;

        yield return new WaitForSeconds(delayNextSpawn);
        spawnDone = true;
    }

    void SpawnFromPool()
    {
        if(enemyType = enemies[0])
        {
            instanceEnemy = PoolEnemyBandit.InstanceEnemyBandit.GetFromPool();
        }
        else
        if(enemyType = enemies[1])
        {
            instanceEnemy = PoolEnemyGoblin.InstanceEnemyGoblin.GetFromPool();
        }
    }
}

