using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WeaponScriptable", menuName = "WeaponScriptableObj")]
public class WeaponScriptableObj : ScriptableObject
{
    public string weaponID;
    public string weaponName;
    public int maxAmmo;
    public float fireRate;
    public float damage;
    public GameObject bullet;
    public float bulletVelocity;
}
