using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class VRTK_GrabWhenTouch : VRTK_InteractableObject {

	public override void StartTouching(VRTK_InteractTouch currentTouchingObject){
		base.StartTouching(currentTouchingObject);
		VRTK_InteractGrab tempGrab = currentTouchingObject.GetComponent<VRTK_InteractGrab>();
		if(tempGrab!=null){
			tempGrab.AttemptGrab();
		}
	}
}
