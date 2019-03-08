using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemyScriptable", menuName = "EnemyScriptableObj")]
public class EnemyScriptableObj : ScriptableObject {

    public string enemyID;
    public string enemyName;
    //public GameObject enemyPrefab;
    public int HP;
    public float speed;
    public float attackRange;
    public float damage;
}
