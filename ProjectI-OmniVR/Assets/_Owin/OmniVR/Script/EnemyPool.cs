using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour {
    [SerializeField]private int EnemyID;
    // Use this for initialization
    Queue<PoolMember> unusedQueue = new Queue<PoolMember>();
    List<PoolMember> inUseList = new List<PoolMember>();
    void Start () {
        while(inUseList.Count>0){
            ReturnToPool(inUseList[0]);
        }
        if(unusedQueue.Count<=0){
            for(int i=0;i<transform.childCount;i++){
                PoolMember tempProj = transform.GetChild(i).GetComponent<PoolMember>();
                tempProj.AssignHandler(this);
                unusedQueue.Enqueue(tempProj);
                tempProj.gameObject.SetActive(false);
            }
        }
    }

    public void ReturnToPool(PoolMember tempProj){
        tempProj.gameObject.SetActive(false);
        inUseList.Remove(tempProj);
        unusedQueue.Enqueue(tempProj);
    }
    public virtual GameObject IssueFromPool(Transform newPosition){
        PoolMember tempProj = unusedQueue.Dequeue();
        tempProj.transform.position = newPosition.position;
        tempProj.transform.rotation = newPosition.rotation;
        //Debug.Log(newPosition.gameObject.name + " with rotations" +newPosition.rotation);
        inUseList.Add(tempProj);
        tempProj.gameObject.SetActive(true);
        return tempProj.gameObject;
    }
    public int GetID(){
        return EnemyID;
    }
}