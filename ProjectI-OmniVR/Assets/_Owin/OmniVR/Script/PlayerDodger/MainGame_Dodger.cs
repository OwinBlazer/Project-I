using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame_Dodger : MonoBehaviour {
	private MatrixReloadedPool bulletPool;
	[SerializeField]float bulletVelocity;
	[SerializeField]float bulletInterval;
	[SerializeField]int minBullet;
	[SerializeField]int maxBullet;
	[SerializeField] Transform leftPosLimit;
	[SerializeField] Transform rightPosLimit;
	private float currTimer;
	// Use this for initialization
	void Start () {
		bulletPool = GetComponent<MatrixReloadedPool>();
		currTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(currTimer<bulletInterval){
			currTimer +=Time.deltaTime;
		}else{
			currTimer -=bulletInterval;
			int spawnQty = Random.Range(minBullet,maxBullet);
			for(int i=0;i<spawnQty;i++){
			float randomX = Random.Range(leftPosLimit.position.x,rightPosLimit.position.x);
			float randomY = Random.Range(leftPosLimit.position.y,rightPosLimit.position.y);
				if((Random.value>0.1)){
					//Debug.Log("Spawn no. "+i+" is red of qty "+ spawnQty);
					//spawn red
					BulletRed tempRed = bulletPool.IssueBulletRed();
					tempRed.GetComponent<Rigidbody>().AddForce(0,0,bulletVelocity);
					tempRed.transform.position = new Vector3 (randomX,randomY,transform.position.z);
				}else{
					//spawn green
					BulletGreen tempGreen = bulletPool.IssueBulletGreen();
					tempGreen.GetComponent<Rigidbody>().AddForce(0,0,bulletVelocity);
					tempGreen.transform.position = new Vector3 (randomX,randomY,transform.position.z);
				}

			}
		}
	}
}
