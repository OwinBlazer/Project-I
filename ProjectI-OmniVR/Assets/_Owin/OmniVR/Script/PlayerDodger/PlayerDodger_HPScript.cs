using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerDodger_HPScript : MonoBehaviour {
[SerializeField]Slider hpBar;
[SerializeField]float hpMax;
[SerializeField]GameOverHandler gameOver;
private float hpCurr;
public static PlayerDodger_HPScript playerDodger_HP;
	// Use this for initialization
	void Start () {
		if(playerDodger_HP==null){
			playerDodger_HP = this;
		}else{
			Destroy(gameObject);
		}
		hpCurr = hpMax;
		UpdateHPBar();
	}
	
	private void UpdateHPBar(){
		hpBar.value = hpCurr/hpMax;
	}

	public void GetHitFor(int damage){
		hpCurr-=damage;
		UpdateHPBar();
		DeathCheck();
	}

	private void DeathCheck(){
		if(hpCurr<=0){
			gameOver.gameObject.SetActive(true);
			gameOver.GameOver();
		}
	}
}
