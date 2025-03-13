using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectsCreatedCount : MonoBehaviour
{
    public int count;
    public TMP_Text objectsCreatedText;

    void Start() {
        count = 0;
    }

    void Update() {
        objectsCreatedText.text = "Объектов построено: " + count.ToString();
    }

    public void IncreaseCount() {
        count++;
    }
}
