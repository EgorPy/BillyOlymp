using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS_Camera;

public class TutorialRoom1 : MonoBehaviour
{
    TutorialManager tm;
    // DoorOpen od;
    CameraMovement cm;
    public GameObject rts_camera;
    public GameObject panel25;
    public GameObject panel26;
    public int mineralsCount;
    public GameObject door;

    void Start() {
        tm = GameObject.FindObjectOfType<TutorialManager>();
        // od = GameObject.FindObjectOfType<DoorOpen>();
        cm = GameObject.FindObjectOfType<CameraMovement>();
    }

    private void OnTriggerEnter()
    {
        func();
    }

    private void OnTriggerStay() {
        func();
    }


    void func() {
        if (tm.popUpIndex == 30) {
            mineralsCount = PlayerPrefs.GetInt("mineralsCount");
            PlayerPrefs.SetInt("mineralsCount", mineralsCount + 710);
            tm.IncrementPopUpIndex();
            if (door != null) {
                door.GetComponent<DoorOpen>().isOpened = true;
            }
            cm._range = new Vector2(90f, 40f);
            cm._pos = new Vector2(398f, 0);
            // rts_camera.GetComponent<CameraMovement>().SetPosition(new Vector3(350f, 0, 0));
            panel25.SetActive(false);
            panel26.SetActive(true);
        }
        // if (tm.popUpIndex == 40) {
        //     this.gameObject.SetActive(false);
        // }
    }
}
