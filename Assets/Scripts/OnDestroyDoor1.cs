using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS_Camera;

public class OnDestroyDoor1 : MonoBehaviour
{
    public GameObject brokenDoor;
    InoMorphInvasion imi;

    void Start() {
        imi = GameObject.FindObjectOfType<InoMorphInvasion>();
    }

    void OnDestroy()
    {
        brokenDoor.SetActive(true);
        imi.StopInvasion();
    }
}
