using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OW_EnemyBulletHit : MonoBehaviour {
	[SerializeField]OW_EnemyStats enemyStats;
    [SerializeField]float dmgMult;
    [SerializeField]OW_HPManager hPManager;
    private void Start()
    {
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Bullet _bullet = collision.gameObject.GetComponent<Bullet>();
            enemyStats.curHP -= _bullet.damage*dmgMult;
            Debug.Log("enemy hit!"+_bullet.damage,gameObject);
            hPManager.ActivateCanvasHP();
			if (enemyStats.curHP <= 0)
			{
				enemyStats.curHP=0;
                OW_LootSpawner.lootSpawner.SpawnItemFor(enemyStats,transform);
				enemyStats.EnemyDeath(_bullet.bulletID);
			}
            // bisa cek bulletID dan level yang mengenai enemy disini
        }
    }
}
