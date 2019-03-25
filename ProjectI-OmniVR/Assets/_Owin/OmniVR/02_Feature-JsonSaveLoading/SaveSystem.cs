using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class SaveSystem:MonoBehaviour {
	[SerializeField]string savePathName;
	public void SaveData(SaveClass saveClass, int slot){
		string savePath = Application.dataPath+"/"+savePathName+"/"+slot.ToString()+"/";
		string writtenSave = JsonUtility.ToJson(saveClass);
		Debug.Log(writtenSave);
		if(!Directory.Exists(savePath)){
			Directory.CreateDirectory(savePath);
		}
		File.WriteAllText(savePath+"sav.dat",writtenSave);
	}

	public SaveClass LoadData(int slot){
		string savePath = Application.dataPath+"/"+savePathName+"/"+slot.ToString()+"/";
		if(Directory.Exists(savePath)){
			string dataText = File.ReadAllText(savePath+"sav.dat");
			SaveClass readData = JsonUtility.FromJson<SaveClass>(dataText);
			return readData;
		}else{
			return null;
		}
	}
}
//SAMPLE FOR FILE SAVING, CHANGE TO FIT NEED@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
public class SaveClass{
	public PlayerDetail playerDetail;
	public BoonDetail boonDetail;
	public int Code = 10;
}

//in order to be able to be saved via json, class must be expressed as System.Serializable
[System.Serializable]
public class PlayerDetail{
	public string name;
	public int age;
}
[System.Serializable]
public class BoonDetail{
	public string boon;
}