using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class Item_HPCrystal : CombatItem {
    [SerializeField]float RecoveryValue;
    [Tooltip("How long the particle effect lasts")][SerializeField]float particleTime;
    [SerializeField]ParticleSystem ps;
    public override void UseItem(object x, InteractableObjectEventArgs y)
    {
        Debug.Log("Healing crystal used! Healed "+RecoveryValue+"HP");
        ps.transform.parent = null;
        ps.Play();
        Invoke("ResetParticle",particleTime);
        base.UseItem(x,y);
    }
    private void ResetParticle(){
        ps.Stop();
        ps.transform.SetParent(transform);
        ps.transform.localPosition = Vector3.zero;
    }
    // Use this for initialization
    void Start () {
		ps.Stop();
	}
	

	
}
