using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS_Camera;

public class OnDestroyDoor2 : MonoBehaviour
{
    public GameObject brokenDoor;

    void OnDestroy()
    {
        brokenDoor.SetActive(true);
    }
}
