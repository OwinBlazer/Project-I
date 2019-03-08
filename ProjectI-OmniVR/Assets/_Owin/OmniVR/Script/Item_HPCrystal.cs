using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class Item_HPCrystal : CombatItem {
    [SerializeField]float RecoveryValue;
    [Tooltip("How long the particle effect lasts")][SerializeField]float particleTime;
    [SerializeField]ParticleSystem particleSystem;
    public override void UseItem(object x, InteractableObjectEventArgs y)
    {
        Debug.Log("Healing crystal used! Healed "+RecoveryValue+"HP");
        particleSystem.transform.parent = null;
        particleSystem.Play();
        Invoke("ResetParticle",particleTime);
        base.UseItem(x,y);
    }
    private void ResetParticle(){
        particleSystem.Stop();
        particleSystem.transform.SetParent(transform);
        particleSystem.transform.localPosition = Vector3.zero;
    }
    // Use this for initialization
    void Start () {
		particleSystem.Stop();
	}
	

	
}
