using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OW_DisableClearParticle : MonoBehaviour {
	[SerializeField]ParticleSystem particleSystem;
	// Use this for initialization
	private void OnEnable(){
		Debug.Log("System has been cleared");
		particleSystem.Clear();
	}
}
