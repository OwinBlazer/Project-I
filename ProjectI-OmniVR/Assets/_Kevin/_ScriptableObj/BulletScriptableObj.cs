using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletScriptable", menuName = "BulletScriptableObj")]
public class BulletScriptableObj : ScriptableObject
{
    public int bulletID;
    public string bulletName;
    public int level;
    public float damage;
    public float delayBeforeDestroy;
    public GameObject impact;
}
