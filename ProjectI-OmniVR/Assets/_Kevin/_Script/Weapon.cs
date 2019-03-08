using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponScriptableObj weaponScriptableObj;
    public string weaponID;
    public int maxAmmo;
    public int currentAmmo;
    public float fireRate;
    public float damage;
    public GameObject bullet;
    public Transform[] shootPos;
    public float bulletVelocity;
    public bool canShoot = true;
    public float velo;
    GameObject instanceBullet;

    void GetDataFromScriptableObject()
    {
        weaponID = weaponScriptableObj.weaponID;
        maxAmmo = weaponScriptableObj.maxAmmo;
        fireRate = weaponScriptableObj.fireRate;
        damage = weaponScriptableObj.damage;
        bullet = weaponScriptableObj.bullet;
        bulletVelocity = weaponScriptableObj.bulletVelocity;
    }

	// Use this for initialization
	void Start ()
    {
        GetDataFromScriptableObject();
        currentAmmo = maxAmmo;
    }
	
	// Update is called once per frame
	void Update ()
    {
        velo = GetComponent<Rigidbody>().velocity.x;

        if (Input.GetKeyDown(KeyCode.S))
        {
            Shoot();
        }

        if(currentAmmo <= 0)
        {
            //Reload();
        }
	}

    void Shoot()
    {
        if (canShoot && currentAmmo > 0)
        {
            foreach (Transform _shootPos in shootPos)
            {
                SpawnFromPool();

                instanceBullet.transform.position = _shootPos.position;
                instanceBullet.transform.rotation = _shootPos.rotation;

                instanceBullet.GetComponent<Rigidbody>().velocity = _shootPos.forward * bulletVelocity;                
            }

            currentAmmo -= 1;
            canShoot = false;
            StartCoroutine(DelayFireRate());
        }
    }

    IEnumerator DelayFireRate()
    {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    void SpawnFromPool()
    {
        if(weaponID == "WE1")
        {
            var _instanceBullet = PoolHandgunBullet.InstanceHandgunBullet.GetFromPool();
            instanceBullet = _instanceBullet;
        }
        else if(weaponID == "WE2")
        {
            var _instanceBullet = PoolShotgunBullet.InstanceShotgunBullet.GetFromPool();
            instanceBullet = _instanceBullet;
        }
        
    }

    //void Reload()
    //{
    //    if (GetComponent<Rigidbody>().velocity.x >= 5) // cek goyang
    //    {
    //        currentAmmo = maxAmmo;
    //    }
    //}
}
