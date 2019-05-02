using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OW_GameOverHandler : MonoBehaviour {
	[SerializeField]Animator animator;
	[SerializeField]QuestTracker questTracker;
	[SerializeField]OW_SceneLoader sceneLoader;
	// Use this for initialization
	void Start () {
		
	}
	
	public void TriggerGameOver(){
		questTracker.ResetProgress();
		animator.SetBool("isTriggered",true);
		Invoke("ReturnToBar",3);
	}

	private void ReturnToBar(){
		sceneLoader.GoToScene("01_Bar");
	}
}
