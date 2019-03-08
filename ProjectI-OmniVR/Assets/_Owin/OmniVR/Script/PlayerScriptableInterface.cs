using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;
public class PlayerScriptableInterface : MonoBehaviour {
	//[SerializeField]PlayerScriptableObject playerScriptable
	[SerializeField]List<VRTK_UIDropZoneSingle> PlayerEquipZone = new List<VRTK_UIDropZoneSingle>();
	[SerializeField]InventoryManager inventoryManager;
	[SerializeField]GameObject templateItem;
	[SerializeField]List<Text> displayTextList;
	//0 = right hand
	//1 = left hand
	//2 = right shoulder
	//3 = left shoulder

	// Use this for initialization
	void Start () {
		//initialize inventory
		InventoryInit();
		EquipmentInit();
	}
	
	private void InventoryInit(){
		//get data from player scriptable object
		//assign the relevant data to each item slot
		List<VRTK_UIDropZoneSingle> tempZoneList = inventoryManager.GetAllSlots();
		int latestEmptyIndex=0;
		//foreach item in player's inventory, @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
		//
		// GameObject tempObject = Instantiate(templateItem,tempZoneList[latestEmptyIndex].transform);
		// ItemData tempData = tempObject.GetComponent<ItemData>();
		// tempObject.GetComponent<VRTK_UIDropDragSingle>().SetNewZone(tempZoneList[latestEmptyIndex]);
		// tempObject.GetComponent<ItemReader>().SetDisplayText(displayTextList);
		// tempData = PlayerScriptableObject.getData;
		// latestEmptyIndex++;
		//foreach end
	}
	private void EquipmentInit(){
		//get equipped data from player scriptable object
		//assign relevant data to the slots
		int latestEmptyIndex=0;
		//foreach item in player's equipmentSlot, @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
		GameObject tempObject = Instantiate(templateItem,PlayerEquipZone[latestEmptyIndex].transform);
		//ItemData tempData = tempObject.GetComponent<ItemData>();
		//if tempData!=null / Check if empty data
		//tempObject.GetComponent<VRTK_UIDropDragSingle>().SetNewZone(tempZoneList[latestEmptyIndex]);
		//tempObject.GetComponent<ItemReader>().SetDisplayText(displayTextList);
		//latestEmptyIndex++;
		//foreach end
	}

	public void SetEquippedData(){
		foreach(VRTK_UIDropZoneSingle dropZone in PlayerEquipZone){
			ItemData tempData = (ItemData)dropZone.GetData();
			//unpack data, send to playerScriptable here @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
		}

	}
}
