using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OW_SceneLoader : MonoBehaviour {

	public void GoToScene(string sceneName){
		SceneManager.LoadScene(sceneName);
	}
}
