using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS_Camera;

public class TutorialRoom2_1 : MonoBehaviour
{
    TutorialManager tm;
    DoorOpen od;
    CameraMovement cm;
    CameraRotation cr;
    MouseManager mm;
    public GameObject rts_camera;

    void Start() {
        tm = GameObject.FindObjectOfType<TutorialManager>();
        od = GameObject.FindObjectOfType<DoorOpen>();
        cm = GameObject.FindObjectOfType<CameraMovement>();
        cr = GameObject.FindObjectOfType<CameraRotation>();
        mm = GameObject.FindObjectOfType<MouseManager>();
    }

    private void OnTriggerEnter()
    {
        func();
    }

    private void OnTriggerStay() {
        func();
    }

    void func() {
        if (tm.popUpIndex == 33) {
            tm.IncrementPopUpIndex();
            // this.gameObject.SetActive(false);
            // od.isOpened = false;
            // cm._range = new Vector2(45f, 112f);
            // cm._pos = new Vector2(338f, 0);
            // cm._targetPosition = new Vector3(330.7f, 0, 0);
            // cr._targetAngle = 160f;
            // if (mm.selectedObject == null) {
            //     rts_camera.transform.position = new Vector3(330.7f, 0, 0);
            // }
        }
    }
}
