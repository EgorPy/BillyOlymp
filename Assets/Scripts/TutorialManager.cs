using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    public int popUpIndex = 0;

    // tutorial variables
    public float panBoardThickness = 2f; // make sure to make it the same as in the CameraMovement.cs script
    public Transform dronePosition;
    public Vector3 gotoPosition;
	MouseManager mm;
    // InoMorphInvasion imi;
    // CameraShake cs;
    public GameObject tutorialRoom2Arrow;
    public GameObject tutorialRoom2Arrow1;
    public GameObject TutorialRoom2Trigger1;
    public GameObject TutorialRoom2Trigger2;
    public GameObject TutorialRoom2Trigger3;
    public GameObject TutorialRoom2Trigger1_1;
    public GameObject TutorialRoom2Trigger2_1;

    void Start () {
		mm = GameObject.FindObjectOfType<MouseManager>();
        // imi = GameObject.FindObjectOfType<InoMorphInvasion>();
        // cs = GameObject.FindObjectOfType<CameraShake>();
	}

    void FixedUpdate() {
        // Debug.Log(popUpIndex);
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex) {
                // if (!popUps[popUpIndex].activeSelf) {
                popUps[i].SetActive(true);
                // }
            } else {
                // if (popUps[popUpIndex].activeSelf) {
                popUps[i].SetActive(false);
                // }
            }
        }

        if (popUpIndex == 1) {
            if (Input.mousePosition.y >= Screen.height - panBoardThickness || 
                Input.mousePosition.y <= panBoardThickness || 
                Input.mousePosition.x >= Screen.width - panBoardThickness || 
                Input.mousePosition.x <= panBoardThickness) {
                popUpIndex++;
            }
        } else if (popUpIndex == 3) {
            if (Input.GetMouseButton(1)) {
                popUpIndex++;
            }
        } else if (popUpIndex == 5) {
            if (Input.GetAxis("Mouse ScrollWheel") != 0) {
                popUpIndex++;
            }
        } else if (popUpIndex == 8) {
            dronePosition.position = Vector3.Lerp(dronePosition.position, gotoPosition, 0.05f);
            if (mm.selectedObject != null) {
                if (mm.selectedObject.name == "Billy") {
                    popUpIndex++;
                }
            }
        } else if (popUpIndex == 30) {
            dronePosition.position = Vector3.Lerp(dronePosition.position, new Vector3(408.08f, dronePosition.position.y, -13.58f), 0.02f);
            dronePosition.rotation = Quaternion.Euler(0, -53.04f, 0);
        } else if (popUpIndex == 31) {
            dronePosition.position = Vector3.Lerp(dronePosition.position, new Vector3(355.8f, dronePosition.position.y, 2), 0.03f);
            dronePosition.rotation = Quaternion.Euler(0, 0, 0);
        } else if (popUpIndex == 33) {
            dronePosition.position = Vector3.Lerp(dronePosition.position, new Vector3(369.44f, dronePosition.position.y, 77.89f), 0.02f);
            dronePosition.rotation = Quaternion.Euler(0, -251.33f, 0);
            tutorialRoom2Arrow.transform.localPosition = new Vector3(-39.60001f, -12.66f, 118f);
            tutorialRoom2Arrow.GetComponent<FloatingAnimation>().startingVal = 3f;
        } else if (popUpIndex == 34) {
            tutorialRoom2Arrow.GetComponent<FloatingAnimation>().startingVal = -60.6f;
        } else if (popUpIndex == 32) {
            // TutorialRoom2Trigger1.SetActive(false);
            // TutorialRoom2Trigger2.SetActive(false);
            // TutorialRoom2Trigger3.SetActive(false);
        } else if (popUpIndex == 47) {
            dronePosition.position = Vector3.Lerp(dronePosition.position, new Vector3(312.72f, dronePosition.position.y, 76.21f), 0.01f);
            TutorialRoom2Trigger1_1.SetActive(true);
            tutorialRoom2Arrow1.transform.localPosition = new Vector3(-99.24002f, -12.66f, 118f);
            tutorialRoom2Arrow1.GetComponent<FloatingAnimation>().startingVal = 3f;
        } else if (popUpIndex == 48) {
            TutorialRoom2Trigger2_1.SetActive(true);
            tutorialRoom2Arrow1.GetComponent<FloatingAnimation>().startingVal = -60.6f;
        } else if (popUpIndex == 55) {
            dronePosition.position = Vector3.Lerp(dronePosition.position, new Vector3(312.86f, dronePosition.position.y, 17.98f), 0.006f);
            dronePosition.rotation = Quaternion.identity;
        } else if (popUpIndex == 58) {
            dronePosition.position = Vector3.Lerp(dronePosition.position, new Vector3(206.4f, dronePosition.position.y, 14f), 0.006f);
        } else if (popUpIndex == 59) {
            dronePosition.position = Vector3.Lerp(dronePosition.position, new Vector3(40.76f, dronePosition.position.y, 1.03f), 0.006f);
        } else if (popUpIndex == 60) {
            dronePosition.position = Vector3.Lerp(dronePosition.position, new Vector3(41.02f, dronePosition.position.y, 25.86f), 0.006f);
        }
    }

    public void IncrementPopUpIndex() {
        popUpIndex++;
        // StartCoroutine(wait(1f));
    }

    public void DecrementPopUpIndex() {
        popUpIndex--;
    }

    public void SetPopUpIndex(int index) {
        popUpIndex = index;
    }

    public void IncrementPopUpIndexTo12() {
        if (popUpIndex == 11) {
            popUpIndex++;
        }
    }

    IEnumerator wait(float seconds) {
        yield return new WaitForSeconds(seconds);
        popUpIndex++;
    }
}
