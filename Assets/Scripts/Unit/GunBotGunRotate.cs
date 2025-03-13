using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBotGunRotate : MonoBehaviour
{
    public GameObject guns;
    public float speed;

    public void Rotate() {
        guns.transform.Rotate(0, 0, speed);
    }
}
