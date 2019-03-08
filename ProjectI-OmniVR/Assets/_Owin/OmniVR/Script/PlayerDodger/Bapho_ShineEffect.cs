using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bapho_ShineEffect : MonoBehaviour {
	[SerializeField]Color shineColor;
	MeshRenderer eyeShine;
	int frameNumber;
	Vector2 currentOffset;
	bool isPlaying;
	// Use this for initialization
	void Start () {
		eyeShine = GetComponent<MeshRenderer>();
		isPlaying = false;
		frameNumber = 8;
		currentOffset = new Vector2(0.75f,0);
	}
	
	// Update is called once per frame
	public void startShine(){
		if(!isPlaying){
			isPlaying = true;
			frameNumber=0;
			StartCoroutine(NextFrame());
		}
	}
	public void startShine(Color newColor){
		if(!isPlaying){
			eyeShine.material.color = newColor;
			isPlaying = true;
			frameNumber=0;
			StartCoroutine(NextFrame());
		}
	}
	public bool GetIsPlaying(){
		return isPlaying;
	}
	IEnumerator NextFrame(){
		while(frameNumber<8){
			currentOffset.x =  (frameNumber%4)*0.25f;
			if(frameNumber<4){
				currentOffset.y = 0.5f;
			}else{
				currentOffset.y = 0;
			}
			eyeShine.material.mainTextureOffset = currentOffset;
			frameNumber++;
			yield return new WaitForEndOfFrame();
		}
		isPlaying = false;
	}
}
