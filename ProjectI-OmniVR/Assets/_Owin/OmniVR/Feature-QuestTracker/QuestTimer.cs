using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class QuestTimer : MonoBehaviour {
	[SerializeField]OW_GameOverHandler gameOverHandler;
	private float timerTarget;
	private int questMode;
	private float startingTime;
	private Coroutine timerCoroutine;
	// Use this for initialization
	void Start () {
		SceneManager.sceneUnloaded +=UnloadCoroutineUponExit;
	}
	
	// Update is called once per frame

	public void StartTimer(int questMode, float timer){
		timerTarget = timer;
		startingTime = Time.timeSinceLevelLoad;
		this.questMode = questMode;
		timerCoroutine = StartCoroutine(TimedEndQuest());
	}
	public void StopTimer(){
		if(timerCoroutine!=null){
			StopCoroutine(timerCoroutine);
			timerCoroutine=null;
		}
	}

	public void UnloadCoroutineUponExit<Scene> (Scene scene){
		StopTimer();
		SceneManager.sceneUnloaded-=UnloadCoroutineUponExit;
	}
	private IEnumerator TimedEndQuest(){
		yield return new WaitForSecondsRealtime (timerTarget);
		Debug.Log("Boeing");
		//CHECK IF MODE
		if(questMode==0){
			//WIN
			QuestTracker.questTracker.ReportSurvival();
			//end wave
		}else{
			//LOSE
			gameOverHandler.TriggerGameOver();
		}
	}
}
