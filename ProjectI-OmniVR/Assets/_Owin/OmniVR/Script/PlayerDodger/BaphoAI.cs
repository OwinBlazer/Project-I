using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
[System.Serializable]
class AIAction{
	public float chance;
	public float attackSpeed;
	public GameObject hitBox;
}
public class BaphoAI : MonoBehaviour {
	[SerializeField]List <AIAction> attackChances = new List<AIAction>();
	[SerializeField]float intervalMin;
	[SerializeField]float intervalMax;
	[SerializeField]Animator anim;
	private float currentInterval;
	private float targetInterval;
	private bool isInAction;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		isInAction = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(currentInterval<targetInterval){
			currentInterval+=Time.deltaTime;
		}else{
			if(!isInAction){

			//roll for Action
			float sum = 0;
			foreach(AIAction action in attackChances){
				sum+=action.chance;
			}
			float rng = Random.Range(0,sum);
			int actionIndex = 0;
			while(rng>attackChances[actionIndex].chance){
				rng-=attackChances[actionIndex].chance;
				actionIndex++;
			}
			switch(actionIndex){
				case 0://attack top
					AttackTop();
				break;
				case 1://attack mid
					AttackBot();
				break;
				case 2://attack blue/orange
				break;
			}
				isInAction =true;
			}else{

			}
		}
	}
	private void resetInterval(){
		anim.SetInteger("ActionID",0);
		currentInterval = 0;
		targetInterval = Random.Range(intervalMin,intervalMax);
		isInAction = false;
	}

	private void AttackTop(){
		anim.SetInteger("ActionID",1);
	}
	public void LaunchAttack(int hitboxID){
		GameObject hitBox = attackChances[hitboxID].hitBox;
		hitBox.transform.position = transform.position;
		hitBox.SetActive(true);
		hitBox.GetComponentInChildren<Bapho_AttackHitbox>().resetHitStatus();
		hitBox.GetComponent<Rigidbody>().velocity = Vector3.zero;
		hitBox.transform.LookAt(VRTK_DeviceFinder.HeadsetCamera().position);
		hitBox.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(VRTK_DeviceFinder.HeadsetCamera().position-transform.position)*attackChances[hitboxID].attackSpeed);
	}
	private void AttackBot(){
		anim.SetInteger("ActionID",2);

	}
}
