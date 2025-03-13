using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOutside : MonoBehaviour
{
    InoMorphInvasion imi;

    void Start() {
        imi = GameObject.FindObjectOfType<InoMorphInvasion>();
    }

    void OnTriggerEnter(Collider other)
    {
        imi.StopInvasion();
    }
}
