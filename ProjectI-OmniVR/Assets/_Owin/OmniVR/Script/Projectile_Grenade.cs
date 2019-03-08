using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Grenade : MonoBehaviour {
GrenadeHandler grenadeHandler;
Rigidbody rigidbody;
private List<Rigidbody> explosionTargets = new List<Rigidbody>();
	[SerializeField]float lifetime;
	[SerializeField] float throwHeight;
	[SerializeField] float throwDistance;
	[SerializeField]float returnDelay;
	[SerializeField]float explosionForce;
	[SerializeField]float explosionRadius;
	[SerializeField]ParticleSystem particleSystem;
	[SerializeField]GameObject model;
	float currLifetime;
	private static Quaternion quaternionZero = Quaternion.Euler(0,0,0);
	Rigidbody rb;
	// Use this for initialization
	void Start () {
		particleSystem.Stop();
		rb = GetComponent<Rigidbody>();
		//InitProjectile();
	}
	
	// Update is called once per frame
	void Update () {
		if (currLifetime==0){
			rb.AddRelativeForce(0,throwHeight,throwDistance);
		}
		
		if(currLifetime<lifetime){
			currLifetime+=Time.deltaTime;
		}else{
			currLifetime = 0;
			Debug.Log("Boom");
			ApplyExplodeForce();
			Invoke("ReturnToPool",particleSystem.main.duration);
		}
	}
	private void ReturnToPool(){
		
			InitProjectile();
			grenadeHandler.ReturnToPool(this);
			Debug.Log("Returned "+gameObject.name);
	}
	public void AssignHandler(GrenadeHandler handler){
		grenadeHandler = handler;
	}

	private void InitProjectile(){
		model.SetActive(true);
		particleSystem.gameObject.SetActive(false);
		particleSystem.Stop();
		particleSystem.gameObject.transform.SetParent(this.transform);
		particleSystem.transform.localPosition = Vector3.zero;
		particleSystem.transform.rotation = Quaternion.Euler(Vector3.zero);
		currLifetime = 0;
		rb.velocity = Vector3.zero;
		rb.rotation = quaternionZero;
		transform.rotation = quaternionZero;
			//resets the settings, makes it ready to be called again
	}

	private void ApplyExplodeForce(){
		particleSystem.gameObject.SetActive(true);
		particleSystem.Play();
		particleSystem.gameObject.transform.SetParent(null);
		particleSystem.transform.rotation = quaternionZero;
		model.SetActive(false);
		foreach(Rigidbody rb in explosionTargets){
			if(rb!=null){
			rb.AddExplosionForce(explosionForce,transform.transform.position,explosionRadius);
			//Debug.Log("applied from "+gameObject.name+" towards "+rb.gameObject.name);
			}
		}
		explosionTargets.Clear();
	}

	private void OnTriggerEnter(Collider col){
		Rigidbody tempRB = col.GetComponent<Rigidbody>();
		if(tempRB!=null){
		//Debug.Log("entered!"+col.gameObject.name);
			explosionTargets.Add(tempRB);
		}
	}
	private void OnTriggerExit(Collider col){
		//Debug.Log("exited!");
		Rigidbody tempRB = col.GetComponent<Rigidbody>();
		if(tempRB!=null){
			explosionTargets.Remove(tempRB);
		}
	}
}
