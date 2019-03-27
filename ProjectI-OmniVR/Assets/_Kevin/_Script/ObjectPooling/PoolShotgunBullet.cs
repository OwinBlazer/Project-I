﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolShotgunBullet : MonoBehaviour {

    public GameObject shotgunBullet;
    public Queue<GameObject> availablePrefab = new Queue<GameObject>();
    public static PoolShotgunBullet InstanceShotgunBullet { get; private set; }

    // Use this for initialization
    void Start()
    {
        InstanceShotgunBullet = this;
        GrowPool();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GrowPool()
    {
        for (int i = 0; i < 15; i++)
        {
            var instanceToAdd = Instantiate(shotgunBullet);
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
        //if (availablePrefab1.Count == 0)
        //    GrowPool();
        var instance = availablePrefab.Dequeue();
        instance.SetActive(true);
        return instance;
    }
}
