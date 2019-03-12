using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMSoundChooser : MonoBehaviour {
	[SerializeField]AudioClip BGMSong;
	[SerializeField]BGMPlayer bgmPlayer;
	// Use this for initialization
	void Start () {
		if(BGMSong!=null){
			bgmPlayer.PlayOverrideBGM(BGMSong);
		}
	}
}
