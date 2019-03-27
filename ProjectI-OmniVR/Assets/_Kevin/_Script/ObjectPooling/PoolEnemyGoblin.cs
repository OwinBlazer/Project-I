using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolEnemyGoblin : MonoBehaviour {

    public GameObject enemyGoblin;
    public Queue<GameObject> availablePrefab = new Queue<GameObject>();
    public static PoolEnemyGoblin InstanceEnemyGoblin { get; private set; }

    // Use this for initialization
    void Start()
    {
        InstanceEnemyGoblin = this;
        GrowPool();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GrowPool()
    {
        for (int i = 0; i < 10; i++)
        {
            var instanceToAdd = Instantiate(enemyGoblin);
            instanceToAdd.transform.SetParent(transform);
            AddToPool(instanceToAdd);
        }
    }

    public void AddToPool(GameObject instance)
    {
        instance.SetActive(false);
        availablePrefab.Enqueue(instance);
    }

    public GameObject GetFromPool()
    {
        //if (availablePrefab.Count == 0)
        //{
        //    GrowPool();
        //}

        var instance = availablePrefab.Dequeue();
        instance.SetActive(true);
        return instance;
    }
}
