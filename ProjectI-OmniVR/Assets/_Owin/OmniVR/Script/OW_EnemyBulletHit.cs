using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OW_EnemyBulletHit : MonoBehaviour {
	[SerializeField]OW_EnemyStats enemyStats;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Bullet _bullet = collision.gameObject.GetComponent<Bullet>();
            enemyStats.HP -= _bullet.damage;

			if (enemyStats.HP <= 0)
			{
				enemyStats.HP=0;
				enemyStats.EnemyDeath();
			}
            // bisa cek bulletID dan level yang mengenai enemy disini
        }
    }
}
