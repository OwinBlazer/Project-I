using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OW_EnemyStats : MonoBehaviour {
	public string enemyID;
    private Animator anim;
    public EnemyScriptableObj enemyScriptbleObj;
	[SerializeField]AI_PlayerFinder playerFinder;

    public Transform player;
    public float attackRange;
    public float HP;
    public float speed;
    public float damage;
	void Start(){
        player = playerFinder.GetTarget();
	}
	// Use this for initialization
	void OnEnable ()
    {
        GetDataFromScriptableObject();

        anim = GetComponent<Animator>();
       // nav.speed = speed;@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
	   //speed contacts the local playerfinder
	   //attackRange contacts the local playerFinder
		playerFinder.GetNavMeshAgent().speed = speed;
		playerFinder.GetNavMeshAgent().stoppingDistance = attackRange;
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
		string parsedStr="";
		for(int i=2;i<enemyID.Length;i++){
			parsedStr+=enemyID[i];
		}
		int result;
		if(int.TryParse(parsedStr, out result)){
			QuestTracker.questTracker.ReportDeath(result);
		}
		playerFinder.SetIsRunning(false);
		gameObject.SetActive(false);
		//death is handled via turning off. Goblin platoon needs to check if all of its children have died before returning to pool@@@@@@@@@@@@@@@@@@@@
    }
}
