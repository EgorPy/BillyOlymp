using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAlign : MonoBehaviour
{
    public GameObject unit;

    // Update is called once per frame
    void Update()
    {
        if (unit != null) {
            this.gameObject.transform.rotation = unit.transform.rotation;
        }
    }
}
