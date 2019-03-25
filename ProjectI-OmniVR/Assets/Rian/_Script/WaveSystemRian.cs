using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystemRian : MonoBehaviour {
    public Transform[] spawners;

    public float timeSpawnMinDirection;
    public float timeSpawnMaxDirection;
    public float timeSpawnEnemyMin;
    public float timeSpawnEnemyMax;

    public GameObject enemy;
    public GameObject[] enemys;
    public int[] enemyChance;
    public GameObject[] specialEnemy;
    public int enemyIndex;

    public bool isStop;

    [SerializeField] float timeDirection;
    [SerializeField] float timeEnemy;

    private int spawnerindex;
    private Transform spawner;
    private int currentSpawnDirection;

    public bool isSpecifiedEnemy;
    public bool isSpecialEnemy;

    public int maxTargetSpecial;
    private int currentTargetSpecial;

    public int chanceMin = 0;
    public int chanceMax = 100;

    Test_Pool testPool;
    [SerializeField] int poolEnemy;

    private void Start()
    {
        testPool = GetComponent<Test_Pool>();
    }

    private void Update()
    {
        timeDirection -= Time.deltaTime;
        timeEnemy -= Time.deltaTime;
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        if(isStop == false)
        {
            if (timeDirection <= 0)
            {
                currentSpawnDirection = Random.Range(1, 3);
                timeDirection = Random.Range(timeSpawnMinDirection, timeSpawnMaxDirection);
            }

            if(timeEnemy <= 0)
            {
                RandomEnemy();
                switch (currentSpawnDirection)
                {
                    case 1:
                        spawnerindex = Random.Range(0, 2);
                        spawner = spawners[spawnerindex];
                        testPool.SpawnEnemy(poolEnemy, spawner);
                        timeEnemy = Random.Range(timeSpawnEnemyMin, timeSpawnEnemyMax);
                        break;
                    case 2:
                        spawnerindex = Random.Range(2, 4);
                        spawner = spawners[spawnerindex];
                        testPool.SpawnEnemy(poolEnemy, spawner);
                        timeEnemy = Random.Range(timeSpawnEnemyMin, timeSpawnEnemyMax);
                        break;
                    case 3:
                        spawnerindex = Random.Range(4, 6);
                        spawner = spawners[spawnerindex];
                        testPool.SpawnEnemy(poolEnemy, spawner);
                        timeEnemy = Random.Range(timeSpawnEnemyMin, timeSpawnEnemyMax);
                        break;
                    default:
                        spawnerindex = Random.Range(0, 2);
                        spawner = spawners[spawnerindex];
                        testPool.SpawnEnemy(poolEnemy, spawner);
                        timeEnemy = Random.Range(timeSpawnEnemyMin, timeSpawnEnemyMax);
                        break;
                }
            }
        }
    }

    public void RandomEnemy()
    {
        SpawnSpecifiedEnemy();
        if (isSpecifiedEnemy == false)
        {
            int enemyRandom = Random.Range(chanceMin, chanceMax);

            if (enemyRandom <= enemyChance[1])
            {
                enemy = enemys[1];
                poolEnemy = 0;
            }
            else if (enemyChance.Length >= 1 && enemyRandom <= enemyChance[2])
            {
                enemy = enemys[2];
                poolEnemy = 0;
            }
            else if (enemyChance.Length >= 2 && enemyRandom <= enemyChance[3])
            {
                enemy = enemys[3];
                poolEnemy = 0;
            }

            if(isSpecialEnemy == true)
            {
                if (enemyRandom > enemyChance[3])
                {
                    enemy = specialEnemy[enemyIndex];
                }
            }
        }

    }

    public void SpawnSpecifiedEnemy()
    {
       if (isSpecifiedEnemy == true)
        {
            enemy = enemys[enemyIndex];
        }
       if(isSpecialEnemy == true)
        {
            chanceMax = 110;
        }
    }
}
