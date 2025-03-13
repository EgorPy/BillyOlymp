using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS_Camera;

public class SelectBilly : MonoBehaviour
{
    CameraMovement cm;
    MouseManager mm;

    void Start() {
        cm = GameObject.FindObjectOfType<CameraMovement>();
        mm = GameObject.FindObjectOfType<MouseManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) {
            cm.SetPosition(this.gameObject.transform.position);
            mm.ClearSelection();
        }
    }
}
