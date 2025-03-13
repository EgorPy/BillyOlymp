using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public GameObject leftDoor, rightDoor;
    public bool isOpened = false;

    void Update()
    {
        if (isOpened && leftDoor.transform.localPosition.z > -1.2) {
            float x1 = leftDoor.transform.localPosition.x;
            float y1 = leftDoor.transform.localPosition.y;
            float z1 = Mathf.Round(leftDoor.transform.localPosition.z);
            float x2 = rightDoor.transform.localPosition.x;
            float y2 = rightDoor.transform.localPosition.y;
            float z2 = Mathf.Round(rightDoor.transform.localPosition.z);
            leftDoor.transform.localPosition = Vector3.Lerp(leftDoor.transform.localPosition, new Vector3(x1, y1, z1 - 1.2f), 0.01f);
            rightDoor.transform.localPosition = Vector3.Lerp(rightDoor.transform.localPosition, new Vector3(x2, y2, z2 + 1.2f), 0.01f);
        } else if (!isOpened && leftDoor.transform.localPosition.z < 0) {
            float x1 = leftDoor.transform.localPosition.x;
            float y1 = leftDoor.transform.localPosition.y;
            float z1 = Mathf.Round(leftDoor.transform.localPosition.z);
            float x2 = rightDoor.transform.localPosition.x;
            float y2 = rightDoor.transform.localPosition.y;
            float z2 = Mathf.Round(rightDoor.transform.localPosition.z);
            leftDoor.transform.localPosition = Vector3.Lerp(leftDoor.transform.localPosition, new Vector3(x1, y1, z1 + 1.2f), 0.01f);
            rightDoor.transform.localPosition = Vector3.Lerp(rightDoor.transform.localPosition, new Vector3(x2, y2, z2 - 1.2f), 0.01f);
        }
    }

    public void Open() {
        isOpened = true;
    }
}
