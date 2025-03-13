using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconScript : MonoBehaviour
{
    public GameObject go;

    void LateUpdate()
    {
        this.gameObject.transform.position = new Vector3(go.transform.position.x, 67, go.transform.position.z);
        this.gameObject.transform.rotation = Quaternion.Euler(90, 90, 0);
    }
}
