using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SaveSampleScript : MonoBehaviour {
	[SerializeField]SaveSystem saveSystem;
	[SerializeField]InputField nameField;
	[SerializeField]InputField ageField;
	[SerializeField]InputField boonField;
	[SerializeField]InputField slotField;
	private int age;
	public void SaveInputs(){
		int slot;
		if(int.TryParse(ageField.text, out age)&&int.TryParse(slotField.text,out slot)){
			SaveClass toBeSaved = new SaveClass();
			PlayerDetail detail= new PlayerDetail();
			BoonDetail boon = new BoonDetail();
			detail.name = nameField.text;
			detail.age = age;
			boon.boon = boonField.text;
			toBeSaved.playerDetail = detail;
			toBeSaved.boonDetail = boon;
			saveSystem.SaveData(toBeSaved,slot);
		}
	}
	public void LoadInputs(){
		int slot;
		if(int.TryParse(slotField.text,out slot)){
		SaveClass saveData = saveSystem.LoadData(slot);
		nameField.text = saveData.playerDetail.name;
		ageField.text = saveData.playerDetail.age.ToString();
		boonField.text = saveData.boonDetail.boon;
		}
	}
}
