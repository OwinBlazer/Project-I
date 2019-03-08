using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using VRTK;

[RequireComponent(typeof(VRTK_UIDropSingleData))]
public class VRTK_UIDropDragSingle : VRTK_UIDraggableItem {
	[SerializeField][Tooltip("Swap content with destination's single drag drop zone if zone is occupied")]private bool swapContent;
	private VRTK_UIDropZoneSingle currentZone;
	private VRTK_UIDropSingleData UIdragData;

	private void Start(){
		SetNewZone(transform.parent.GetComponent<VRTK_UIDropZoneSingle> ());
		UIdragData = GetComponent<VRTK_UIDropSingleData> ();
	}
	public void SetNewZone(VRTK_UIDropZoneSingle newZone){
		currentZone = newZone;
		currentZone.SetOccupyingDrag (this);
	}
	public VRTK_UIDropSingleData GetDropData(){
		return UIdragData;
	}
	public override void OnEndDrag(PointerEventData eventData){
		canvasGroup.blocksRaycasts = true;
		dragTransform = null;
		transform.position += (transform.forward * forwardOffset);
		bool validDragEnd = true;
		if (restrictToDropZone)
		{
			if (validDropZone != null && validDropZone != startDropZone)
			{

				//the only new part of the whole "custom" script :D <==========================================================================================================
				VRTK_UIDropZoneSingle singleDropZone = validDropZone.GetComponent<VRTK_UIDropZoneSingle>();
				if (singleDropZone != null) {
					if (swapContent&&singleDropZone.GetOccupyingDrag()!=null ) {
						//set target's content's parent to ours
						singleDropZone.GetOccupyingDrag().transform.SetParent(transform.parent);
						transform.SetParent (validDropZone.transform);

						//swap the contents, adjust their zones
						VRTK_UIDropZoneSingle tempSwapZone = singleDropZone;
						VRTK_UIDropDragSingle tempSwapDrag = singleDropZone.GetOccupyingDrag();

						currentZone.SetOccupyingDrag (tempSwapDrag);
						tempSwapDrag.currentZone = currentZone;

						tempSwapZone.SetOccupyingDrag (this);
						currentZone = tempSwapZone;
					} else {
						transform.SetParent (validDropZone.transform);
						singleDropZone.SetOccupyingDrag (this);
						currentZone.SetOccupyingDrag (null);
						currentZone = singleDropZone;
					}
				}else {
					ResetElement();
					validDragEnd = false;
				}
			}
			else
			{
				ResetElement();
				validDragEnd = false;
			}
		}

		Canvas destinationCanvas = (eventData.pointerEnter != null ? eventData.pointerEnter.GetComponentInParent<Canvas>() : null);
		if (restrictToOriginalCanvas)
		{
			if (destinationCanvas != null && destinationCanvas != startCanvas)
			{
				ResetElement();
				validDragEnd = false;
			}
		}

		if (destinationCanvas == null)
		{
			//We've been dropped off of a canvas
			ResetElement();
			validDragEnd = false;
		}

		if (validDragEnd)
		{
			VRTK_UIPointer pointer = GetPointer(eventData);
			if (pointer != null)
			{
				pointer.OnUIPointerElementDragEnd(pointer.SetUIPointerEvent(pointer.pointerEventData.pointerPressRaycast, gameObject));
			}
			OnDraggableItemDropped(SetEventPayload(validDropZone));
		}

		validDropZone = null;
		startParent = null;
		startCanvas = null;
	}
}
