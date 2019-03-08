using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SampleDataReader : MonoBehaviour {
[SerializeField]VRTK_UIDropZoneSingle dropZone;
[SerializeField] Text nameField;
[SerializeField] Text powerField;
[SerializeField] Text rateField;
[SerializeField] Text ammoField;

	
	// Use this for initializationa
	void Start () {
		dropZone = GetComponent<VRTK_UIDropZoneSingle> ();
	}
	
	public void UpdateText(){
		
	//-----------------------------VV TypeCasting menjadi tipe data yang baru, setelah di extend
		
	SampleData data = (SampleData)dropZone.GetData ();
		
	if (data != null) {
				
	nameField.text = data.nama;
				
	powerField.text = data.damage.ToString();
				
	rateField.text = data.kecepatanTembak.ToString();
				
	ammoField.text = data.peluru.ToString();


		}
	}
	public void ReadDataOf(VRTK_UIDropSingleData dataHandler)
	{
		SampleData data = (SampleData)dataHandler;
		if (data != null)
		{
			nameField.text = data.nama;
			powerField.text = data.damage.ToString();
			rateField.text = data.kecepatanTembak.ToString();
			ammoField.text = data.peluru.ToString();
		}
	}
}
