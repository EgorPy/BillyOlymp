using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS_Camera;

public class TutorialRoom2 : MonoBehaviour
{
    TutorialManager tm;
    // DoorOpen od;
    CameraMovement cm;
    CameraRotation cr;
    MouseManager mm;
    public GameObject rts_camera;
    public GameObject door;

    void Start() {
        tm = GameObject.FindObjectOfType<TutorialManager>();
        // od = GameObject.FindObjectOfType<DoorOpen>();
        cm = GameObject.FindObjectOfType<CameraMovement>();
        cr = GameObject.FindObjectOfType<CameraRotation>();
        mm = GameObject.FindObjectOfType<MouseManager>();
    }

    private void OnTriggerEnter()
    {
        // if (tm.popUpIndex == 31 || tm.popUpIndex == 32) {
            // tm.IncrementPopUpIndex();
            if (door != null) {
                door.GetComponent<DoorOpen>().isOpened = false;
            }
            if (cm._range.x != 54f && cm._range.x != 400f) {
                cm._range = new Vector2(54f, 112f);
                cm._pos = new Vector2(359f, 0);
                rts_camera.GetComponent<CameraMovement>().SetTargetPosition(new Vector3(350f, 0, 0));
                cr._targetAngle = 160f;
                if (mm.selectedObject == null) {
                    rts_camera.GetComponent<CameraMovement>().SetTargetPosition(new Vector3(350f, 0, 0));
                }
            }
            this.gameObject.SetActive(false);
        // }
    }
}
