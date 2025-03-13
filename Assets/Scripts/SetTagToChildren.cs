using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTagToChildren : MonoBehaviour
{
    public string tag;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform t in transform)
{
    t.gameObject.tag = tag;
}
gameObject.tag = tag;
    }

}
