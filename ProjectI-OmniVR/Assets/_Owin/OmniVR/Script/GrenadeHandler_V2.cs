using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class GrenadeHandler_V2 : MonoBehaviour {
	[SerializeField]int poolSize;
	[SerializeField]GameObject grenadePrefab;
	// Use this for initialization
	Queue<Projectile_Grenade_V2> unusedQueue = new Queue<Projectile_Grenade_V2>();
	List<Projectile_Grenade_V2> inUseList = new List<Projectile_Grenade_V2>();
	void Start () {
		if(inUseList.Count>0){
			for(int i=0;i<inUseList.Count;i++){
				ReturnToPool(inUseList[0]);
			}
		}
		if(unusedQueue.Count<=0){
			for(int i=0;i<poolSize;i++){
				Projectile_Grenade_V2 tempProj = Instantiate(grenadePrefab,transform).GetComponent<Projectile_Grenade_V2>();
				tempProj.AssignHandler(this);
				unusedQueue.Enqueue(tempProj);
				tempProj.gameObject.SetActive(false);
			}
		}
	}

	public void ReturnToPool(Projectile_Grenade_V2 tempProj){
		tempProj.gameObject.SetActive(false);
		inUseList.Remove(tempProj);
		unusedQueue.Enqueue(tempProj);
	}
	public Projectile_Grenade_V2 IssueFromPool(VRTK_InteractGrab grabber){
		Projectile_Grenade_V2 tempProj = unusedQueue.Dequeue();
		tempProj.SetCtrlr(grabber);
		tempProj.gameObject.SetActive(true);
		tempProj.transform.position = grabber.transform.position;
		tempProj.transform.rotation = grabber.transform.rotation;
		//Debug.Log(newPosition.gameObject.name + " with rotations" +newPosition.rotation);
		inUseList.Add(tempProj);
		return tempProj;
	}
}
