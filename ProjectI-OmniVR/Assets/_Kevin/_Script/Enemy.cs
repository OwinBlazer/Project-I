using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public EnemyScriptableObj enemyScriptbleObj;
    public string enemyID;

    NavMeshAgent nav;
    Transform player;

    Animator anim;
    public bool walking, attacking, dead;

    float attackRange;
    float speed;
    float damage;

    float maxHP;
    public float curHP;
    public GameObject canvasHP;
    public Text textHP;
    public Image imageHP;
    public float timerCanvasHP = 2;

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
        if (curHP > 0)
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
        }
        else
        {
            EnemyDeath();
        }
    }

    void GetDataFromScriptableObject()
    {
        enemyID = enemyScriptbleObj.enemyID;
        maxHP = enemyScriptbleObj.HP;
        curHP = enemyScriptbleObj.HP;
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

        canvasHP.SetActive(false);
        Destroy(gameObject, 5);

        //call quest tracker, send this enemy ID
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.tag == "Bullet")
        //{
        //    Bullet _bullet = collision.gameObject.GetComponent<Bullet>();
        //    curHP -= _bullet.damage;

        //    StartCoroutine(ActivateCanvasHP());
        //    // bisa cek bulletID dan level yang mengenai enemy disini
        //}
    }

    public IEnumerator ActivateCanvasHP()
    {
        canvasHP.SetActive(true);
        imageHP.fillAmount = curHP / maxHP;
        textHP.text = "HP : " + curHP.ToString();

        yield return new WaitForSeconds(timerCanvasHP);
        canvasHP.SetActive(false);
    }

    void AddEnemyToPool()
    {
        //GetDataFromScriptableObject();

        //if (enemyID == "EN1")
        //{
        //    PoolEnemyBandit.InstanceEnemyBandit.AddToPool(gameObject);
        //}
        //else if (enemyID == "EN2")
        //{
        //    PoolEnemyGoblin.InstanceEnemyGoblin.AddToPool(gameObject);
        //}
    }
}
