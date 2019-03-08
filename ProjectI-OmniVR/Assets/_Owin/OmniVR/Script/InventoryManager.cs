using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
	private List<VRTK_UIDropZoneSingle> inventoryList = new List<VRTK_UIDropZoneSingle>();
	// Use this for initialization
	void Start () {
		for(int i=0;i<transform.childCount;i++){
			inventoryList.Add(transform.GetChild(i).GetComponent<VRTK_UIDropZoneSingle>());
		}
	}

	public List<VRTK_UIDropZoneSingle> GetAllSlots(){
		return inventoryList;
	}
}
