using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    private NavMeshAgent nav;
    private Animator anim;
    public EnemyScriptableObj enemyScriptbleObj;

    public bool walking, attacking, dead;

    public Transform player;
    public float attackRange;
    public int HP;
    public float speed;
    public float damage;
    
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

        if(HP == 0)
        {
            EnemyDeath();
        }
    }

    void GetDataFromScriptableObject()
    {
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

        nav.enabled = false;
        Destroy(GetComponent<BoxCollider>());

        //call quest tracker, send this enemy ID
    }
}
