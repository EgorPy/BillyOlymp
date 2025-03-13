using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBreakDoor : MonoBehaviour
{
    TutorialManager tm;
    public GameObject panel50;
    public GameObject panel51;

    void Start() {
        tm = GameObject.FindObjectOfType<TutorialManager>();
    }

    void OnDestroy() {
        if (tm.popUpIndex == 57) {
            tm.IncrementPopUpIndex();
            panel50.SetActive(false);
            panel51.SetActive(true);
        }
    }
}
