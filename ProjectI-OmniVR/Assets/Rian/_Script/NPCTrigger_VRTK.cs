namespace VRTK.Examples
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;


    public class NPCTrigger_VRTK : MonoBehaviour
    {
        public VRTK_InteractableObject linkedObject;
        public NPCTrigger npcTrigger;

        protected virtual void OnEnable()
        {
            linkedObject = (linkedObject == null ? GetComponent<VRTK_InteractableObject>() : linkedObject);

            if (linkedObject != null)
            {
                linkedObject.InteractableObjectUsed += InteractableObjectUsed;
                linkedObject.InteractableObjectUnused += InteractableObjectUnused;
            }
            npcTrigger = GetComponent<NPCTrigger>();
        }

        protected virtual void OnDisable()
        {
            if (linkedObject != null)
            {
                linkedObject.InteractableObjectUsed -= InteractableObjectUsed;
                linkedObject.InteractableObjectUnused -= InteractableObjectUnused;
            }
        }

        protected virtual void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
        {
            npcTrigger.TriggeredObject();
        }

        protected virtual void InteractableObjectUnused(object sender, InteractableObjectEventArgs e)
        {

        }
    }
}
