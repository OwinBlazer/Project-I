using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class QuestTimer : MonoBehaviour {
	[SerializeField]private float timerTarget;
	private int questMode;
	private float startingTime;
	private Coroutine timerCoroutine;
	// Use this for initialization
	void Start () {
		SceneManager.sceneUnloaded +=UnloadCoroutineUponExit;
	}
	
	// Update is called once per frame

	public void StartTimer(int questMode){
		startingTime = Time.timeSinceLevelLoad;
		this.questMode = questMode;
		timerCoroutine = StartCoroutine(TimedEndQuest());
	}
	public void StopTimer(){
		StopCoroutine(timerCoroutine);
	}

	public void UnloadCoroutineUponExit<Scene> (Scene scene){
		StopTimer();
		SceneManager.sceneUnloaded-=UnloadCoroutineUponExit;
	}
	private IEnumerator TimedEndQuest(){
		yield return new WaitForSecondsRealtime (timerTarget);
		//CHECK IF MODE
		if(questMode==0){
			//WIN
			QuestTracker.questTracker.ReportSurvival();
			//end wave
		}else{
			//LOSE
			//trigger loss;@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
		}
	}
}
