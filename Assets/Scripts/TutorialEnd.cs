using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using RTS_Camera;
using TMPro;

public class TutorialEnd : MonoBehaviour
{
    public GameObject spaceShip;
    public GameObject rts_camera;
    public GameObject arrow;
    public GameObject ui;
    public GameObject buttonPlay;
    public GameObject buttonStop;
    public GameObject blocksEngine2;
    public GameObject volumeParent;
    public Volume volume;
    public GameObject levelStats;

    // level stats
    public TMP_Text levelPassedText;
    public TMP_Text enemiesKilledText;
    public TMP_Text objectsCreatedText;
    public TMP_Text gameTimeText;
    public Image buttonImage;
    public GameObject exitButton;
    public TMP_Text exitText;
    public GameObject popUpCanvas;
    float alpha1, alpha2, alpha3, alpha4, alpha5, alpha6 = 0;

    MouseManager mm;
    bool end = false;
    float counter = 0;
    float angle = 0;

    void Start() {
        mm = GameObject.FindObjectOfType<MouseManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.transform.name);
        if (other.gameObject.transform.name == "Billy") {
            other.gameObject.transform.parent.gameObject.SetActive(false);
            arrow.SetActive(false);
            ui.SetActive(false);
            buttonPlay.SetActive(false);
            buttonStop.SetActive(false);
            blocksEngine2.SetActive(false);
            popUpCanvas.SetActive(false);
            mm.selectedObject = null;
            gameTimeText.GetComponent<GameTimer>().updateTimer = false;

            StartCoroutine(waitSeat(1f));
            StartCoroutine(waitEnd(2f));
        }
    }

    IEnumerator waitSeat(float seconds) {
        yield return new WaitForSeconds(seconds);
        rts_camera.GetComponent<CameraMovement>()._targetPosition = spaceShip.transform.position;
        rts_camera.GetComponent<CameraRotation>().SetRotation(Mathf.Clamp(rts_camera.GetComponent<CameraRotation>()._targetAngle, 0, 360));
        rts_camera.GetComponent<CameraRotation>()._targetAngle = 137f;
        angle = rts_camera.GetComponent<CameraRotation>()._targetAngle;
        volumeParent.SetActive(true);
    }

    IEnumerator waitEnd(float seconds) {
        yield return new WaitForSeconds(seconds);
        end = true;
    }

    void Update() {
        if (end) {
            rts_camera.GetComponent<CameraMovement>()._targetPosition = spaceShip.transform.position;
            rts_camera.GetComponent<CameraRotation>()._targetAngle = angle;
            spaceShip.transform.position = Vector3.Lerp(spaceShip.transform.position, new Vector3(249f, 73f, 100f), 0.002f);
            rts_camera.GetComponent<CameraZoom>().SetZoom(-0.1f);
            if (counter < 10f) {
                counter += Time.deltaTime;
            }
            if (counter > 2f && volume.weight < 1) {
                volume.weight += Time.deltaTime / 4;
                levelStats.SetActive(true);
            }
            if (counter > 7.3f && alpha1 < 254) {
                alpha1 += Time.deltaTime;
                levelPassedText.color = new Color(255f, 255f, 255f, alpha1);
            }
            if (counter > 8f && alpha2 < 254) {
                alpha2 += Time.deltaTime;
                enemiesKilledText.color = new Color(255f, 255f, 255f, alpha2);
            }
            if (counter > 8.3f && alpha3 < 254) {
                alpha3 += Time.deltaTime;
                objectsCreatedText.color = new Color(255f, 255f, 255f, alpha3);
            }
            if (counter > 8.6f && alpha4 < 254) {
                alpha4 += Time.deltaTime;
                gameTimeText.color = new Color(255f, 255f, 255f, alpha4);
            }
            if (counter > 8.9 && alpha5 < 254) {
                alpha5 += Time.deltaTime;
                exitText.color = new Color(255f, 255f, 255f, alpha5);
            }
            if (counter > 8.9f && counter < 9.2f && alpha6 < 66f) {
                exitButton.SetActive(true);
                alpha6 += 0.01f;
                Color col = buttonImage.color;
                col.a = alpha6;
                buttonImage.color = col;
            }
        }
    }
}
