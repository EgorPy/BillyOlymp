using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS_Camera;

public class TutorialRoom2_2 : MonoBehaviour
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

    private void OnTriggerEnter(Collider other) {
        func(other);
    }

    private void OnTriggerStay(Collider other) {
        func(other);
    }

    void func(Collider other) {
        if (other.gameObject.name == "Base" && tm.popUpIndex == 35) {
            tm.IncrementPopUpIndex();
            // this.gameObject.SetActive(false);
        }
    }
}
