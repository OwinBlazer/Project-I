using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class OW_PointerGunSwitcher : MonoBehaviour {
    [SerializeField] VRTK_Pointer[] PointerComponentList;
    [SerializeField] GameObject[] GunObjectList;
    private List<VRTK.VRTK_ControllerEvents.ButtonAlias> activationTrigger = new List<VRTK.VRTK_ControllerEvents.ButtonAlias>();
    private void Start()
    {
        for(int i=0;i<PointerComponentList.Length;i++){
            activationTrigger.Add(PointerComponentList[i].activationButton);
        }
        SwitchToGun();
    }
    public void SwitchToGun()
    {
        for(int i=0;i<PointerComponentList.Length;i++){
            PointerComponentList[i].activationButton = VRTK.VRTK_ControllerEvents.ButtonAlias.Undefined;
        }
        foreach(GameObject go in GunObjectList)
        {
            go.SetActive(true);
        }
    }

    public void SwitchToPointer()
    {
        int i=0;
        foreach(VRTK_Pointer p in PointerComponentList)
        {
            p.activationButton = activationTrigger[i];
            i++;
        }
        foreach (GameObject go in GunObjectList)
        {
            go.SetActive(false);
        }
    }
}
