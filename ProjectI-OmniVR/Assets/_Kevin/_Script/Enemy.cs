using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
    public string enemyID;
    private NavMeshAgent nav;
    private Animator anim;
    public EnemyScriptableObj enemyScriptbleObj;

    public bool walking, attacking, dead;

    public Transform player;
    public float attackRange;
    public float HP;
    public float speed;
    public float damage;
    public Text textHP;

	// Use this for initialization
	void Start ()
    {
        GetDataFromScriptableObject();

        anim = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        nav.speed = speed;
        nav.acceleration = speed;      

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            walking = false;
            attacking = true;
            dead = false;

            nav.velocity = Vector3.zero;
            nav.isStopped = true;

            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", true);
        }
        else
        {
            walking = true;
            attacking = false;
            dead = false;

            nav.SetDestination(player.position);

            if (anim.enabled)
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isAttacking", false);
            }
        }

        if (HP <= 0)
        {
            EnemyDeath();
            textHP.text = "";
        }
        else
        {
            textHP.text = "HP : " + HP.ToString();
        }
    }

    void GetDataFromScriptableObject()
    {
        enemyID = enemyScriptbleObj.enemyID;
        HP = enemyScriptbleObj.HP;
        attackRange = enemyScriptbleObj.attackRange;
        speed = enemyScriptbleObj.speed;
        damage = enemyScriptbleObj.damage;
    }

    public void EnemyDeath()
    {
        walking = false;
        attacking = false;
        dead = true;

        anim.SetBool("isWalking", false);
        anim.SetBool("isAttacking", false);
        anim.SetBool("isDead", true);

        nav.velocity = Vector3.zero;
        nav.isStopped = true;
        Destroy(GetComponent<BoxCollider>());

        Destroy(gameObject, 5);
        //Invoke("AddEnemyToPool", 2);
        //call quest tracker, send this enemy ID
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Bullet _bullet = collision.gameObject.GetComponent<Bullet>();
            HP -= _bullet.damage;

            // bisa cek bulletID dan level yang mengenai enemy disini
        }
    }

    void AddEnemyToPool()
    {
        GetDataFromScriptableObject();

        if (enemyID == "EN1")
        {
            PoolEnemyBandit.InstanceEnemyBandit.AddToPool(gameObject);
        }
        else if (enemyID == "EN2")
        {
            PoolEnemyGoblin.InstanceEnemyGoblin.AddToPool(gameObject);
        }
    }
}
