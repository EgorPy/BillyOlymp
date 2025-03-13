using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBreakDoor1 : MonoBehaviour
{
    TutorialManager tm;
    public GameObject panel51;
    public GameObject panel52;

    void Start() {
        tm = GameObject.FindObjectOfType<TutorialManager>();
    }

    void OnDestroy() {
        if (tm.popUpIndex == 58) {
            tm.IncrementPopUpIndex();
            panel51.SetActive(false);
            panel52.SetActive(true);
        }
    }
}
