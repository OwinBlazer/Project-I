using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControllerLaserPointer : PointerInputModule {
    public Camera ControllerCamera;
    public GameObject OVRController;
    //1
    public GameObject reticle;
    public Transform laserTransform;
    //2
    private Transform OVRControllerTransform;
    //3
    public float reticleSizeMultiplier = 0.02f; // The size of the reticle will get scaled with this value
                                                //4
    private PointerEventData pointerEventData;
    //5
    private RaycastResult currentRaycast;
    //6
    private GameObject currentLookAtHandler;

    public float distanceMultiplier;

    // Use this for initialization

    void Awake()
    {
        OVRControllerTransform = OVRController.transform;
    }

    public override void Process()
    {
        HandleLook();
        HandleSelection();
    }

    void HandleLook()
    {
        if (pointerEventData == null)
        {
            pointerEventData = new PointerEventData(eventSystem);
        }

        pointerEventData.position = ControllerCamera.ViewportToScreenPoint(new Vector3(.5f, .5f)); // Set a virtual pointer to the center of the screen
        List<RaycastResult> raycastResults = new List<RaycastResult>(); // A list to hold all the raycast results
        eventSystem.RaycastAll(pointerEventData, raycastResults); // Do a raycast using all enabled raycasters in the scene
        currentRaycast = pointerEventData.pointerCurrentRaycast = FindFirstRaycast(raycastResults); // Get the first hit an set both the local and pointerEventData results

        reticle.transform.position = OVRControllerTransform.position + (OVRControllerTransform.forward * (currentRaycast.distance * distanceMultiplier)); // Move reticle

        laserTransform.position = Vector3.Lerp(OVRControllerTransform.position, reticle.transform.position, .5f);
        laserTransform.LookAt(reticle.transform);
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y, currentRaycast.distance * distanceMultiplier);

        float reticleSize = currentRaycast.distance * distanceMultiplier * reticleSizeMultiplier;
        reticle.transform.localScale = new Vector3(reticleSize, reticleSize, reticleSize); //Scale reticle so it's always the same size

        ProcessMove(pointerEventData); // Pass the pointer data to the event system so entering and exiting of objects is detected
    }

    void HandleSelection()
    {
        if (pointerEventData.pointerEnter != null)
        {
            //Get the OnPointerClick handler of the entered object
            currentLookAtHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(pointerEventData.pointerEnter);

            if (currentLookAtHandler != null && OVRInput.GetDown(OVRInput.RawButton.A))
            {
                // Object in sight with a OnPointerClick handler & pressed the main button
                ExecuteEvents.ExecuteHierarchy(currentLookAtHandler, pointerEventData, ExecuteEvents.pointerClickHandler);
            }
        }
        else
        {
            currentLookAtHandler = null;
        }
    }
}