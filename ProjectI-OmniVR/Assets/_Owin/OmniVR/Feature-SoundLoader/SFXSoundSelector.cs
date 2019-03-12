using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXSoundSelector : MonoBehaviour {
	[SerializeField]private SFXPlayer sfxPlayer;
	[SerializeField]private List<AudioClip> AudioList = new List<AudioClip>();
	
	public void PlayAudioWithID(int ID){
		sfxPlayer.PlaySound(AudioList[ID]);
	}

	
}
