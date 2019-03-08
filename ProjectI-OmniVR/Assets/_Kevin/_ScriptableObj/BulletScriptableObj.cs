using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletScriptable", menuName = "BulletScriptableObj")]
public class BulletScriptableObj : ScriptableObject
{
    public string bulletID;
    public string bulletName;
    public float delayBeforeDestroy;
    public GameObject impact;
}
