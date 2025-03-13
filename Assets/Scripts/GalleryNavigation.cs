using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS_Camera;
using TMPro;

public class GalleryNavigation : MonoBehaviour
{
    public Transform[] cameraPositions;
    public string[] GameObjectNames;
    public TMP_Text gameObjectNameText;
    int cameraPositionIndex;
    CameraMovement cm;

    void Start() {
        cm = GameObject.FindObjectOfType<CameraMovement>();
    }

    public void IncrementCameraPositionIndex() {
        if (cameraPositionIndex != cameraPositions.Length - 1) {
            cameraPositionIndex++;
        } else {
            cameraPositionIndex = 0;
        }
        gameObjectNameText.text = GameObjectNames[cameraPositionIndex];
        cm.SetTargetPosition(cameraPositions[cameraPositionIndex].position);
    }

    public void DecrementCameraPositionIndex() {
        if (cameraPositionIndex != 0) {
            cameraPositionIndex--;
        } else {
            cameraPositionIndex = cameraPositions.Length - 1;
        }
        gameObjectNameText.text = GameObjectNames[cameraPositionIndex];
        cm.SetTargetPosition(cameraPositions[cameraPositionIndex].position);
    }
}
