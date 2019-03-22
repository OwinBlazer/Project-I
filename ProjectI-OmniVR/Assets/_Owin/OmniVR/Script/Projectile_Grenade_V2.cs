using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class Projectile_Grenade_V2 : MonoBehaviour {
GrenadeHandler_V2 grenadeHandler;
private List<Rigidbody> explosionTargets = new List<Rigidbody>();
	[SerializeField]float lifetime;
	[SerializeField]float explosionForce;
	[SerializeField]float explosionRadius;
	[SerializeField]ParticleSystem ps;
	[SerializeField]GameObject model;
	[SerializeField]SphereCollider explosionCollider;
	float currLifetime;
	private VRTK_InteractableObject obj;
	private VRTK_InteractGrab myGrab;
	private static Quaternion quaternionZero = Quaternion.Euler(0,0,0);
	private float colliderOriSize;
	Rigidbody rb;
	// Use this for initialization
	void Start () {
		ps.Stop();
		rb = GetComponent<Rigidbody>();
		obj = GetComponent<VRTK_InteractableObject>();
		colliderOriSize = explosionCollider.radius;
		//InitProjectile();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(currLifetime<lifetime){
			currLifetime+=Time.deltaTime;
		}else{
			currLifetime = 0;
			Debug.Log("Boom");
			ApplyExplodeForce();
			Invoke("ReturnToPool",ps.main.duration);
		}
	}
	private void OnCollisionEnter(Collision col){
			//Debug.Log("Collided!");
			ApplyExplodeForce();
			Invoke("ReturnToPool",ps.main.duration);
	}
	private void ReturnToPool(){
		
			InitProjectile();
			grenadeHandler.ReturnToPool(this);
			//Debug.Log("Returned "+gameObject.name);
	}
	public void AssignHandler(GrenadeHandler_V2 handler){
		grenadeHandler = handler;
	}

	private void InitProjectile(){
		explosionCollider.radius = colliderOriSize;
		obj.isGrabbable = true;
		model.SetActive(true);
		ps.gameObject.SetActive(false);
		ps.Stop();
		ps.gameObject.transform.SetParent(this.transform);
		ps.transform.localPosition = Vector3.zero;
		ps.transform.rotation = Quaternion.Euler(Vector3.zero);
		currLifetime = 0;
		rb.velocity = Vector3.zero;
		rb.rotation = quaternionZero;
		transform.rotation = quaternionZero;
			//resets the settings, makes it ready to be called again
	}

	private void ApplyExplodeForce(){
		ps.gameObject.SetActive(true);
		ps.Play();
		ps.gameObject.transform.SetParent(null);
		ps.transform.rotation = quaternionZero;
		model.SetActive(false);
		foreach(Rigidbody rb in explosionTargets){
			if(rb!=null){
			rb.AddExplosionForce(explosionForce,transform.transform.position,explosionRadius);
			//Debug.Log("applied from "+gameObject.name+" towards "+rb.gameObject.name);
			}
		}
		explosionTargets.Clear();
	}
	public void AttemptToBeGrabbed(){
		myGrab.AttemptGrab();
		Debug.Log("Attempted when "+myGrab.GetGrabbedObject().name);
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

	public void DisableGrab(){
		obj.isGrabbable = false;
	}

	public void SetCtrlr(VRTK_InteractGrab grabber){
		myGrab = grabber;

	}
}
