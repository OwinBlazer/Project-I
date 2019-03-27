using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjSphere : MonoBehaviour {
    public float radius;
    public Collider[] hitColliders;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update ()
    {
        DestroyObjectNearPlayer();
    }


    void DestroyObjectNearPlayer()
    {
        hitColliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider col in hitColliders)
        {
            if (col.gameObject.tag == "WorldObject")
            {
                col.gameObject.SetActive(false);
                //Destroy(col.gameObject);
            }
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.tag == "WorldObject")
    //    {
    //        other.gameObject.SetActive(false);
    //    }
    //}
}
