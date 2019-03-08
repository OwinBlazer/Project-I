using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public abstract class CombatItem : MonoBehaviour {

	Combat_ItemHandler itemHandler;	VRTK_InteractableObject VRTKObj;
	[SerializeField]int qty;
	private void Start(){
	itemHandler = FindObjectOfType<Combat_ItemHandler>();
		VRTKObj = GetComponent<VRTK_InteractableObject>();
	}
	public virtual void UseItem(object x, InteractableObjectEventArgs y){
		//force release by grabber
		VRTKObj.GetGrabbingObject().GetComponent<VRTK_InteractGrab>().ForceRelease();
		//reduce quantity
		qty--;
		if(qty<=0){
			gameObject.SetActive(false);
		}
	}
	public int GetQty(){
		return qty;
	}
	public void SetQty(int newVal){
		qty = newVal;
	}
	public void ChangeQty(int delta){
		qty +=delta;
	}
	private void OnEnable(){
		VRTKObj = (VRTKObj == null ? GetComponent<VRTK_InteractableObject>() : VRTKObj);

		if (VRTKObj != null) {
			VRTKObj.InteractableObjectUsed += UseItem;
		}
	}
	private void OnDisable(){
		if (VRTKObj != null) {
			VRTKObj.InteractableObjectUsed -= UseItem;
		}
	}

	public void DestroyClone(){
		//used to destroy clone when cloned by the grabevent
		Debug.Log("destroy called! for "+name);
		//Destroy(gameObject);
	}

	public GameObject GetGrabberCtrl(){
		return VRTKObj.GetGrabbingObject();
	}
}
