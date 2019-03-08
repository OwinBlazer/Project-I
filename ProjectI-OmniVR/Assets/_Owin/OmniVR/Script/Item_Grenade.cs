using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Item_Grenade : CombatItem {
	[SerializeField]GrenadeHandler grenadeBallHandler;
    public override void UseItem(object x, InteractableObjectEventArgs y)
    {
        //Debug.Log("Grenade used!");
		//spawn one grenade
		grenadeBallHandler.IssueFromPool(base.GetGrabberCtrl().transform);
        base.UseItem(x,y);

    }
}
