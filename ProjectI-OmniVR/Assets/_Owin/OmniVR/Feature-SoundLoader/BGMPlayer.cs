using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour {
	private float maxVolume;
	public static BGMPlayer bgmPlayer;
	[SerializeField]PlayerSettingData myData;
	[SerializeField]AudioSource audioSource;

	[SerializeField]AudioSource subSource;
	bool isFadeSwitching;
	// Use this for initialization
	void Awake () {
		if(bgmPlayer==null){
			bgmPlayer = this;
			DontDestroyOnLoad(gameObject);
		}else{
			Destroy(gameObject);
		}
	}

	private void Start(){
		isFadeSwitching = false;

		//change the maxVolume to comply with the max volume from SETTING@@@@@@@@@@@@@@@@@@@@@@@@@@@
		maxVolume = myData.BGMVolume;
		audioSource.volume = maxVolume;
	}
	
	public void PlayOverrideBGM(AudioClip newClip){
		//immediately plays next song, cutting the current playing song
		audioSource.clip = newClip;
		audioSource.Play();
	}
	public void PlayFadeChangeBGM(AudioClip newClip, float fadeDur, float transitionSmoothness){
		if(!isFadeSwitching){
			//fades out current song to play next song
			subSource.clip = newClip;
			StartCoroutine(FadeSwitch(fadeDur, transitionSmoothness));
		}
	}
	public void PlayFadeInBGM(AudioClip newClip, float fadeDur, float transitionSmoothness){
		audioSource.clip = newClip;
		audioSource.volume = 0;
		if(!isFadeSwitching){
			StartCoroutine(FadeIn(fadeDur,transitionSmoothness));
		}
	}
	public void StopBGMImmediate(){
		audioSource.Stop();
	}
	public void StopBGMFade(float fadeDur, float transitionSmoothness){
		if(!isFadeSwitching){
			StartCoroutine(FadeOut(fadeDur,transitionSmoothness));
		}
	}

	private IEnumerator FadeOut(float fadeDur, float transitionSmoothness){
		isFadeSwitching = true;
		WaitForSeconds waitTime = new WaitForSeconds(Mathf.Clamp(fadeDur/transitionSmoothness,0.01f,fadeDur));
		while(audioSource.volume>0){
			audioSource.volume-=Mathf.Clamp(maxVolume/transitionSmoothness,0.01f,audioSource.volume);
			if(audioSource.volume<0){
				audioSource.volume = 0;
			}
			yield return waitTime;
		}
		audioSource.volume = maxVolume;
		audioSource.Stop();
		isFadeSwitching = false;
	}
	private IEnumerator FadeIn(float fadeDur, float transitionSmoothness){
		audioSource.Play();
		isFadeSwitching = true;
		WaitForSeconds waitTime = new WaitForSeconds(Mathf.Clamp(fadeDur/transitionSmoothness,0.01f,fadeDur));
		while(audioSource.volume<maxVolume){
			audioSource.volume+=Mathf.Clamp(maxVolume/transitionSmoothness,0.01f,maxVolume-audioSource.volume);
			if(audioSource.volume>maxVolume){
				audioSource.volume = maxVolume;
			}
			yield return waitTime;
		}
		isFadeSwitching = false;
	}
	private IEnumerator FadeSwitch( float fadeDur, float transitionSmoothness){
		isFadeSwitching = true;
		subSource.volume = 0;
		subSource.Play();
		WaitForSeconds waitTime = new WaitForSeconds(Mathf.Clamp(fadeDur/transitionSmoothness,0.01f,fadeDur));
		float volumeChange = Mathf.Clamp(maxVolume/transitionSmoothness,0.01f,maxVolume-subSource.volume);
		while(subSource.volume<maxVolume){
			//start the transition process;
			subSource.volume+= volumeChange;
			audioSource.volume -= volumeChange;
			if(subSource.volume>maxVolume){
				subSource.volume = maxVolume;
			}
			if(audioSource.volume<0){
				audioSource.volume = 0;
			}
			yield return waitTime;
		}
		//fadeSwitching is complete, switch audio sources
		isFadeSwitching = false;
		AudioSource tempSource = audioSource;
		audioSource = subSource;
		subSource = tempSource;
		subSource.Stop();
	}

	public void SetVolume(float newVol){
		audioSource.volume = newVol;
		maxVolume = newVol;
	}
}
