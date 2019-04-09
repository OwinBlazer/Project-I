using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OW_HPManager : MonoBehaviour {
	[SerializeField] GameObject canvasHP;
    [SerializeField] Text textHP;
    [SerializeField] Image imageHP;
    [SerializeField] float timerCanvasHP;
	[SerializeField] OW_EnemyStats enemyStats;
	private float currTimer;
	private void Start(){
		currTimer  = timerCanvasHP+1;
	}
	private void Update(){
		if(currTimer<timerCanvasHP&&canvasHP.activeSelf){
			currTimer +=Time.deltaTime;
		}else{
			currTimer=0;
        	canvasHP.SetActive(false);
		}
	}
    public void ActivateCanvasHP()
    {
        canvasHP.SetActive(true);
        imageHP.fillAmount = enemyStats.curHP / enemyStats.maxHP;
        textHP.text = "HP : " + enemyStats.curHP.ToString();
		currTimer=0;
    }
}
