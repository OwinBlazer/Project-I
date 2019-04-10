using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ToProcedural : MonoBehaviour {

    public PlayerSO playerSO;
    [SerializeField]string proceduralSceneName;
    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            QuestDone();
        }
    }

    public void QuestDone()
    {
        var listing = playerSO.activQuest;
        foreach (Quest player in listing)
        {
            player.questStatus = 2;
        }
    }
    public void ExitBar(){
        if(playerSO.priorityQuest!=null&&playerSO.priorityQuest.sceneToLoad.Length>0){
            LoadPriority(playerSO.priorityQuest.sceneToLoad);
        }else{
            LoadProcedural();
        }
    }
    private void LoadProcedural(){
        SceneManager.LoadScene(proceduralSceneName);
    }
    private void LoadPriority(string sceneName){
        SceneManager.LoadScene(sceneName);
    } 
}
