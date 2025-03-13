using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAnimation : MonoBehaviour
{
    public float speed;

    void Update()
    {
        this.gameObject.transform.Rotate(0, speed, 0);
    }
}
