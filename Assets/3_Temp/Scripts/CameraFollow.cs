using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
public Transform follow;
public Vector3 offset;
public float smoothTime = 0.3F;
private Vector3 velocity = Vector3.zero;






    void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, follow.position + offset, ref velocity, smoothTime);
    }




}