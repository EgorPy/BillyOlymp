using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDoorAlreadyDestroyed : MonoBehaviour
{
    public GameObject door;
    public GameObject panel50;
    TutorialManager tm;

    void Start() {
        tm = GameObject.FindObjectOfType<TutorialManager>();
    }

    void Update()
    {
        if (tm.popUpIndex == 57) {
            if (door == null) {
                tm.IncrementPopUpIndex();
                panel50.SetActive(false);
            }
        }
    }
}
