using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletScriptableObj bulletScriptableObj;
    public int bulletID;
    public int level;
    public float damage;
    public float delayBeforeDestroy;
    public GameObject impact;

	// Use this for initialization
	void Start ()
    {
        GetDataFromScriptableObject();
    }

    void GetDataFromScriptableObject()
    {
        bulletID = bulletScriptableObj.bulletID;
        damage = bulletScriptableObj.damage;
        delayBeforeDestroy = bulletScriptableObj.delayBeforeDestroy;
        impact = bulletScriptableObj.impact;
    }

    private void Update()
    {
        delayBeforeDestroy -= Time.deltaTime;

        if(delayBeforeDestroy <= 0)
        {
            GetDataFromScriptableObject(); //untuk reset delay before destroy

            AddBulletToPool();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        AddBulletToPool();
    }

    void AddBulletToPool()
    {
        if (bulletID == 1)
        {
            PoolHandgunBullet.InstanceHandgunBullet.AddToPool(gameObject);
        }
        else if (bulletID == 2)
        {
            PoolShotgunBullet.InstanceShotgunBullet.AddToPool(gameObject);
        }
    }
}
