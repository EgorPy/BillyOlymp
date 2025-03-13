using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS_Camera;

public class OnDestroyDoor : MonoBehaviour
{
    public GameObject brokenDoor;
    public GameObject rts_camera;
    public GameObject spider1;
    public GameObject spider2;

    public GameObject room2trigger1;
    public GameObject room2trigger2;
    public GameObject room2trigger3;

    void OnDestroy()
    {
        brokenDoor.SetActive(true);
        rts_camera.GetComponent<CameraMovement>()._pos = new Vector2(88f, 0);
        rts_camera.GetComponent<CameraMovement>()._range = new Vector2(400f, 190f);
        spider1.SetActive(true);
        spider2.SetActive(true);
        room2trigger1.SetActive(false);
        room2trigger2.SetActive(false);
        room2trigger3.SetActive(false);
    }
}
