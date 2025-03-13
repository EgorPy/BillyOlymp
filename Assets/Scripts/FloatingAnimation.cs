using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingAnimation : MonoBehaviour
{
    public float speed;
    public float amplitude;
    public float startingVal;

    void Start() {
        startingVal = transform.position.y;
    }

    void Update() {
        Sine();
    }

    void Sine() {
        float x = transform.position.x;
        float z = transform.position.z;
        float y = Mathf.Sin(Time.time * speed) * amplitude;

        transform.position = new Vector3(x, startingVal + y, z);
    }
}
