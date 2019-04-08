using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class Weapon : MonoBehaviour
{
    VRTK_ControllerEvents ControllerEvents;
    public WeaponScriptableObj weaponScriptableObj;
    public int weaponID;
    public int level;
    public int maxAmmo;
    public int currentAmmo;
    public float fireRate;
    public float damage;
    public Transform[] shootPos;
    public float bulletVelocity;
    public bool canShoot = true;
    Vector3 velo;
    public float minVelocityToReload;
    GameObject instanceBullet;

    public Text textAmmo;

    void GetDataFromScriptableObject()
    {
        weaponID = weaponScriptableObj.weaponID;
        level = weaponScriptableObj.level;
        maxAmmo = weaponScriptableObj.maxAmmo;
        fireRate = weaponScriptableObj.fireRate;
        //damage = weaponScriptableObj.damage;
        bulletVelocity = weaponScriptableObj.bulletVelocity;
    }

	// Use this for initialization
	void Start ()
    {
        ControllerEvents = transform.parent.GetComponent<VRTK_ControllerEvents>();

        GetDataFromScriptableObject();
        currentAmmo = maxAmmo;        
    }
	
	// Update is called once per frame
	void Update ()
    {       
        if (Input.GetKeyDown(KeyCode.S) || ControllerEvents.triggerPressed)
        {
            Shoot();
        }

        if(currentAmmo <= 0)
        {
            Reload();
            textAmmo.text = "Reload";
        }
        else
        {
            textAmmo.text = "Ammo : " + currentAmmo.ToString();
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
        if(weaponID == 1)
        {
            instanceBullet = PoolHandgunBullet.InstanceHandgunBullet.GetFromPool();
        }
        else if(weaponID == 2)
        {
            instanceBullet = PoolShotgunBullet.InstanceShotgunBullet.GetFromPool();
        }

        instanceBullet.GetComponent<Bullet>().level = level;
    }

    void Reload()
    {
        StartCoroutine("CalcVelocity");

        if (velo.x >= minVelocityToReload || velo.y >= minVelocityToReload || velo.z >= minVelocityToReload) //cek goyang
        {
            currentAmmo = maxAmmo;
            StopCoroutine("CalcVelocity");
            velo = new Vector3(0, 0, 0);
        }
    }

    IEnumerator CalcVelocity()
    {
        while (Application.isPlaying)
        {
            // Position at frame start
            Vector3 prevPos = transform.position;
            // Wait till it the end of the frame
            yield return new WaitForEndOfFrame();
            // Calculate velocity: Velocity = DeltaPosition / DeltaTime
            velo = (prevPos - transform.position) / Time.deltaTime;
        }
    }
}
