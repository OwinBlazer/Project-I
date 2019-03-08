using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletScriptableObj bulletScriptableObj;
    public string bulletID;
    public float delayBeforeDestroy;
    public GameObject impact;

	// Use this for initialization
	void Start ()
    {
        GetDataFromScriptableObject();
    }

    private void Update()
    {
        delayBeforeDestroy -= Time.deltaTime;

        if(delayBeforeDestroy <= 0)
        {
            GetDataFromScriptableObject();
            PoolShotgunBullet.InstanceShotgunBullet.AddToPool(gameObject);
        }
    }
    void GetDataFromScriptableObject()
    {
        bulletID = bulletScriptableObj.bulletID;
        delayBeforeDestroy = bulletScriptableObj.delayBeforeDestroy;
        impact = bulletScriptableObj.impact;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (bulletID == "BU1")
        {
            PoolHandgunBullet.InstanceHandgunBullet.AddToPool(gameObject);
        }
        else if (bulletID == "BU2")
        {
            PoolShotgunBullet.InstanceShotgunBullet.AddToPool(gameObject);
        }
    }
}
