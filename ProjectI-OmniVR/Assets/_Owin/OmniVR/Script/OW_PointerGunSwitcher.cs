using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OW_PointerGunSwitcher : MonoBehaviour {
    [SerializeField] MonoBehaviour[] PointerComponentList;
    [SerializeField] GameObject[] GunObjectList;
    private void Start()
    {
        SwitchToGun();
    }
    public void SwitchToGun()
    {
        foreach(MonoBehaviour m in PointerComponentList)
        {
            m.enabled = false;
        }
        foreach(GameObject go in GunObjectList)
        {
            go.SetActive(true);
        }
    }

    public void SwitchToPointer()
    {
        foreach (MonoBehaviour m in PointerComponentList)
        {
            m.enabled = true;
        }
        foreach (GameObject go in GunObjectList)
        {
            go.SetActive(false);
        }
    }
}
