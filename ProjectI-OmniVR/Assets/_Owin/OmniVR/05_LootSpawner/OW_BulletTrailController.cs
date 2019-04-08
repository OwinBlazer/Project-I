using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OW_BulletTrailController : MonoBehaviour {
	[SerializeField]TrailRenderer trail;
	// Use this for initialization
	void OnEnable () {
		trail.Clear();
		trail.emitting=false;
		trail.colorGradient.alphaKeys[0].alpha=1;
		Invoke("StartTrail",Time.deltaTime);
	}
	private void StartTrail(){
		trail.emitting=true;
	}
	// Update is called once per frame
	void Update () {
		trail.colorGradient.alphaKeys[0].alpha-=0.1f*Time.deltaTime;
	}
}
