using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuButtonsFadeIn : MonoBehaviour
{
    public TMP_Text startGameButton;
    public TMP_Text sandBoxButton;
    public TMP_Text galleryButton;
    public TMP_Text settingsButton;
    public TMP_Text exitButton;

    float counter = 0;
    float a1, a2, a3, a4, a5 = 0;
    bool start = false;

    void Update()
    {
        if (!start) {
            if (counter < 1f) {
                counter += Time.deltaTime;
            } else {
                counter = 0;
                start = true;
            }
        }
        if (start && counter < 10f) {
            counter += Time.deltaTime;

            if (counter >= 0.2f && counter < 0.4f && a1 < 254) {
                startGameButton.transform.parent.gameObject.SetActive(true);
                a1 += Time.deltaTime * 6;
                startGameButton.color = new Color(255f, 255f, 255f, a1);
            }
            if (counter >= 0.4f && counter < 0.6f && a2 < 254) {
                sandBoxButton.transform.parent.gameObject.SetActive(true);
                a2 += Time.deltaTime * 6;
                sandBoxButton.color = new Color(255f, 255f, 255f, a2);
            }
            if (counter >= 0.6f && counter < 0.8f && a3 < 254) {
                galleryButton.transform.parent.gameObject.SetActive(true);
                a3 += Time.deltaTime * 6;
                galleryButton.color = new Color(255f, 255f, 255f, a3);
            }
            if (counter >= 0.8f && counter < 1f && a4 < 254) {
                settingsButton.transform.parent.gameObject.SetActive(true);
                a4 += Time.deltaTime * 6;
                settingsButton.color = new Color(255f, 255f, 255f, a4);
            }
            if (counter >= 1f && counter < 1.2f && a5 < 254) {
                exitButton.transform.parent.gameObject.SetActive(true);
                a5 += Time.deltaTime * 6;
                exitButton.color = new Color(255f, 255f, 255f, a5);
            }
            
            // if (counter > 1f && counter < 2f) {
            //     startGameButton.transform.parent.gameObject.SetActive(true);
            //     sandBoxButton.transform.parent.gameObject.SetActive(true);
            //     galleryButton.transform.parent.gameObject.SetActive(true);
            //     exitButton.transform.parent.gameObject.SetActive(true);
            // }
        }
    }
}
