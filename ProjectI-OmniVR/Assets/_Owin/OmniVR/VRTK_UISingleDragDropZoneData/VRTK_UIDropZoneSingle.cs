using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using VRTK;
public class VRTK_UIDropZoneSingle : VRTK_UIDropZone {
	private VRTK_UIDropDragSingle currentDrag;

	public VRTK_UIDropDragSingle GetOccupyingDrag(){
		return currentDrag;
	}

	public void SetOccupyingDrag(VRTK_UIDropDragSingle newDrag){
		currentDrag = newDrag;
	}

	public VRTK_UIDropSingleData GetData(){
		if (currentDrag != null) {
			return currentDrag.GetDropData ();
		} else {
			return null;
		}
	}
}
