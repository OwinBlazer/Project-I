using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemReader : MonoBehaviour {
	[SerializeField] List<Text> displayText;
	private ItemData itemData;
	private void Start(){
		itemData = GetComponent<ItemData>();
	}
	public void DisplayDataWeapon(){
		displayText[0].text = itemData.weaponName;
		displayText[1].text = itemData.damage.ToString();
		displayText[2].text = itemData.fireRate.ToString();
		//add as necessary
	}

	public void SetDisplayText(List<Text> textList){
		displayText = textList;
	}

	public void DisplayDataWeaponSimple(){

		displayText[0].text = itemData.weaponName;
		displayText[1].text = itemData.damage.ToString();
		displayText[2].text = itemData.fireRate.ToString();
		displayText[3].text = itemData.bulletClip.ToString();
		Debug.Log(itemData.bulletClip.ToString());
	}
	public void UpdateEquipment(){
		FindObjectOfType<PlayerScriptableInterface>().SetEquippedData();
	
	}
}
