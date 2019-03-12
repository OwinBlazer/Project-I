using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour {
	[SerializeField]PlayerSettingData myData;
	[SerializeField]AudioSource audioSource;
	private float maxVolume;
	private void Start(){
		//change the maxVolume to comply with the max volume from SETTING@@@@@@@@@@@@@@@@@@@@@@@@@@@
		maxVolume = myData.SFXVolume;
		audioSource.volume = maxVolume;
	}
	public void PlaySound(AudioClip newClip){
		audioSource.PlayOneShot(newClip);
	}
	public void SetVolume(float newVol){
		audioSource.volume = newVol;
		maxVolume = newVol;
	}
}
