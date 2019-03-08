using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverHandler : MonoBehaviour {
	[SerializeField]List<GameObject> toBeDisabled = new List<GameObject>();
	[SerializeField]GameObject toBeShown;
	// Use this for initialization
	public void GameOver(){
		foreach(GameObject go in toBeDisabled){
			go.SetActive(false);
		}
		toBeShown.SetActive(true);
	}
}
