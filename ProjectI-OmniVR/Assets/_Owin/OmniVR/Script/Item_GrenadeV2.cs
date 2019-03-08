using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Item_GrenadeV2 : CombatItem {
	[SerializeField]GrenadeHandler_V2 grenadeBallHandler;
    public override void UseItem(object x, InteractableObjectEventArgs y)
    {
        //Debug.Log("Grenade used!");
		//spawn one grenade
		Projectile_Grenade_V2 tempObj = grenadeBallHandler.IssueFromPool(base.GetGrabberCtrl().GetComponent<VRTK_InteractGrab>());
		
        base.UseItem(x,y);
		//tempObj.AttemptToBeGrabbed();
    }
}
