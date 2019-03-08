using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeHandler : MonoBehaviour {
	[SerializeField]int poolSize;
	[SerializeField]GameObject grenadePrefab;
	// Use this for initialization
	Queue<Projectile_Grenade> unusedQueue = new Queue<Projectile_Grenade>();
	List<Projectile_Grenade> inUseList = new List<Projectile_Grenade>();
	void Start () {
		if(inUseList.Count>0){
			for(int i=0;i<inUseList.Count;i++){
				ReturnToPool(inUseList[0]);
			}
		}
		if(unusedQueue.Count<=0){
			for(int i=0;i<poolSize;i++){
				Projectile_Grenade tempProj = Instantiate(grenadePrefab,transform).GetComponent<Projectile_Grenade>();
				tempProj.AssignHandler(this);
				unusedQueue.Enqueue(tempProj);
				tempProj.gameObject.SetActive(false);
			}
		}
	}

	public void ReturnToPool(Projectile_Grenade tempProj){
		tempProj.gameObject.SetActive(false);
		inUseList.Remove(tempProj);
		unusedQueue.Enqueue(tempProj);
	}
	public Projectile_Grenade IssueFromPool(Transform newPosition){
		Projectile_Grenade tempProj = unusedQueue.Dequeue();
		tempProj.gameObject.SetActive(true);
		tempProj.transform.position = newPosition.position;
		tempProj.transform.rotation = newPosition.rotation;
		//Debug.Log(newPosition.gameObject.name + " with rotations" +newPosition.rotation);
		inUseList.Add(tempProj);
		return tempProj;
	}
}
