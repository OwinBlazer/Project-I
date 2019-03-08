using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Combat_ItemHandler : MonoBehaviour {
	//load
	// Use this for initialization
	[SerializeField]List<CombatItem> combatItemList;
	[SerializeField]Transform leftItem;
	[SerializeField]Transform rightItem;
	void Start () {
		//loadItem from playerInventory scriptableObject
		foreach(CombatItem CI in combatItemList){
			//replace this "3" with loaded value!!
			CI.SetQty(3);
		}
		//set left item and right item. Load from Player Scriptable object. @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
		//combatItemList[leftItemID].gameObject.SetParent(leftItem);
		//combatItemList[rightItemID].gameObject.SetParent(rightItem);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetQtyOfItem(int itemID, int newVal){
		combatItemList[itemID].SetQty(newVal);
		QtyActivationCheck();
	}
	public void ChangeQtyOfItem(int itemID, int newVal){
		combatItemList[itemID].ChangeQty(newVal);
		QtyActivationCheck();
	}
	private void QtyActivationCheck(){
		foreach(CombatItem CI in combatItemList){
			if(CI.GetQty()>0){
				CI.gameObject.SetActive(true);
			}else{
				CI.gameObject.SetActive(false);
			}
		}
	}
}
