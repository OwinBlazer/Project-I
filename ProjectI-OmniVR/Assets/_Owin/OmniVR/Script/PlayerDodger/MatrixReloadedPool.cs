using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixReloadedPool : MonoBehaviour {
	public static MatrixReloadedPool bulletPool;
	[SerializeField]GameObject BulletRed;
	[SerializeField]int redPoolSize;
	[SerializeField]GameObject BulletGreen;
	[SerializeField]int greenPoolSize;
	Queue<BulletGreen> greenPoolUnused = new Queue<BulletGreen>();
	List<BulletGreen> greenPoolUsed = new List<BulletGreen>();
	Queue<BulletRed> redPoolUnused = new Queue<BulletRed>();
	List<BulletRed> redPoolUsed = new List<BulletRed>();

	// Use this for initialization
	void Start () {
		if(bulletPool!=null){
			Destroy(gameObject);
		}else{
			bulletPool = this;
		}
		for(int i=0;i<redPoolSize;i++){
			GameObject tempObj = Instantiate(BulletRed,transform);
			redPoolUnused.Enqueue(tempObj.GetComponent<BulletRed>());
			tempObj.SetActive(false);
		}
		for(int i=0;i<greenPoolSize;i++){
			GameObject tempObj = Instantiate(BulletGreen,transform);
			greenPoolUnused.Enqueue(tempObj.GetComponent<BulletGreen>());
			tempObj.SetActive(false);
		}
	}

	public BulletRed IssueBulletRed(){
		BulletRed tempBullet =  redPoolUnused.Dequeue();
		redPoolUsed.Add(tempBullet);
		tempBullet.gameObject.SetActive(true);
		return tempBullet;
	}

	public BulletGreen IssueBulletGreen(){
		BulletGreen tempBullet =  greenPoolUnused.Dequeue();
		greenPoolUsed.Add(tempBullet);
		tempBullet.gameObject.SetActive(true);
		return tempBullet;
	}

	public void ReturnBulletRed(BulletRed bullet){
		redPoolUsed.Remove(bullet);
		redPoolUnused.Enqueue(bullet);
		bullet.gameObject.SetActive(false);
		bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
	}
	public void ReturnBulletGreen(BulletGreen bullet){
		greenPoolUsed.Remove(bullet);
		greenPoolUnused.Enqueue(bullet);
		bullet.gameObject.SetActive(false);
		bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
		
	}
}
