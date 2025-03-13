using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject confirmMenu;
    public GameObject canvas;
    public GameObject settingsCanvas;
    int qualityIndex = 4;

    public void ToggleQualityIndex() {
        if (qualityIndex == 4) {
            qualityIndex = 2;
        } else if (qualityIndex == 2) {
            qualityIndex = 0;
        } else {
            qualityIndex = 4;
        }
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void StartGame() {
        SceneManager.LoadScene("Bunker");
    }

    public void SandBox() {
        SceneManager.LoadScene("SandBox");
    }

    public void Gallery() {
        SceneManager.LoadScene("Gallery");
    }

    public void OpenMenu() {
        SceneManager.LoadScene("Menu");
    }

    public void Settings() {
        canvas.SetActive(false);
        settingsCanvas.SetActive(true);
    }

    public void CloseSettings() {
        canvas.SetActive(true);
        settingsCanvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (SceneManager.GetActiveScene().name != "Menu") {
                if (!confirmMenu.activeSelf) {
                    ShowConfirmMenu();
                } else {
                    HideConfirmMenu();
                }
            }
        }
    }

    public void ShowConfirmMenu() {
        confirmMenu.SetActive(true);
    }

    public void HideConfirmMenu() {
        confirmMenu.SetActive(false);
    }
}
