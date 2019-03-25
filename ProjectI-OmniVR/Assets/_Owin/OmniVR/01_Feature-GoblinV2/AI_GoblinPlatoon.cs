using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_GoblinPlatoon : MonoBehaviour {
	[SerializeField] float aggroLevel;
	[SerializeField] float attackingInterval;
	List<AI_GoblinAttack> goblinAttackList = new List<AI_GoblinAttack>();
	List<int> attackingGoblinIndex = new List<int>();
	int isEngagingCount;
	private float currTimer;
	private List<Vector3> relativeSpawnPos = new List<Vector3>();
	// Use this for initialization
	void Awake () {
		for(int i=0;i<transform.childCount;i++){
			relativeSpawnPos.Add(transform.GetChild(i).transform.localPosition);
		}
		InitializeParameter();
	}
	private void OnEnable(){
		InitializeParameter();
	}
	public void InitializeParameter(){
		isEngagingCount=0;
		currTimer=0;
		for(int i=0;i<transform.childCount;i++){
			AI_GoblinAttack tempGoblin = transform.GetChild(i).GetComponent<AI_GoblinAttack>();
			if(tempGoblin!=null){
				goblinAttackList.Add(tempGoblin);
			}
			Transform childObj = transform.GetChild(i);
			childObj.localRotation = Quaternion.identity;
			childObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
			childObj.GetComponent<AI_PlayerFinder>().GetNavMeshAgent().nextPosition = transform.position;
			childObj.localPosition = relativeSpawnPos[i];
		}
		//@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@RESETTHESTATSOFTHEGOBLINS@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
		
	}
	// Update is called once per frame
	void Update () {
		UpdateEngagingCount();
		if(isEngagingCount>0){
			currTimer+=Time.deltaTime;
			if(currTimer>attackingInterval){
				List<int> allGoblinIndex = new List<int>();
				attackingGoblinIndex.Clear();

				//lists living and ready@@@@@@@@@@@@@@@@
				for(int i=0;i<goblinAttackList.Count;i++){
					if(goblinAttackList[i].gameObject.activeSelf&&goblinAttackList[i].GetIsEngaging()){
						allGoblinIndex.Add(i);
					}
				}
				int attackingQty=0;
				//choose attacking number of goblins
				//increase likeliness of higher amounts by using Aggro Level
				attackingQty = (int)Mathf.Floor(Random.Range(1,allGoblinIndex.Count*aggroLevel));
				if(attackingQty>allGoblinIndex.Count){
					attackingQty = allGoblinIndex.Count;
				}

				//lists attacking goblins index, based from living goblins
				for(int i=0;i<attackingQty;i++){
					int rng = Random.Range(0,allGoblinIndex.Count);
					int tempIndex = allGoblinIndex[rng];
					allGoblinIndex.RemoveAt(rng);
					attackingGoblinIndex.Add(tempIndex);
				}
				//choose attackers
				foreach(int index in attackingGoblinIndex){
					//Debug.Log("Triggered in "+index);
					goblinAttackList[index].TriggerAttack();
				}

				//reset timer
				currTimer=0;
			}
		}else{
			currTimer=0;
		}
	}
	public void SetGoblinParam(float aggroLv, float atkInterval){
		aggroLevel = aggroLv;
		attackingInterval = atkInterval;
	}
	public void SetGoblinQty(int num){
		int setQty = num;
		if(num>transform.childCount){
			setQty = transform.childCount;
		}
		for(int i=0;i<transform.childCount;i++){
			bool isActivated = true;
			if(i>num){
				isActivated = false;
			}
			transform.GetChild(i).gameObject.SetActive(isActivated);
		}
	}
	private void UpdateEngagingCount(){
		isEngagingCount=0;
		foreach(AI_GoblinAttack gob in goblinAttackList){
			if(gob.gameObject.activeSelf){
				if(gob.GetIsEngaging()){
					isEngagingCount++;
				}
			}
		}
	}
}
