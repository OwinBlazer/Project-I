using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusicLoader : MonoBehaviour {
	[SerializeField]AudioClip BGMusicClip;
	[SerializeField]BGMPlayer bgmPlayer;
	// Use this for initialization
	void Start () {
		
	}

	public void SwitchSong(){
		bgmPlayer.PlayFadeChangeBGM(BGMusicClip,3,10);
	}
	public void StopSong(){
		bgmPlayer.StopBGMFade(3,10);
	}
}
