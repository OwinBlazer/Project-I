using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudioOption : MonoBehaviour {
	[SerializeField]PlayerSettingData myData;
	[SerializeField]Slider bgmSlider;
	[SerializeField]Slider sfxSlider;
	[SerializeField]BGMPlayer bgmPlayer;
	[SerializeField]SFXPlayer sfxPlayer;
	// Use this for initialization
	void Start () {
		bgmSlider.value = myData.BGMVolume;
		sfxSlider.value = myData.SFXVolume;
	}

	public void UpdateSFXValue(){
		myData.SFXVolume = sfxSlider.value;
		sfxPlayer.SetVolume(sfxSlider.value);
	}

	public void UpdateBGMValue(){
		myData.BGMVolume = bgmSlider.value;
		bgmPlayer.SetVolume(bgmSlider.value);
	}
}
