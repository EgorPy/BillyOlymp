using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideProgramming : MonoBehaviour
{
    public GameObject blocksSelectionCanvas;
    public GameObject programmingEnv;
    public Button button;

    void Start() {
        button.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick() {
        if (blocksSelectionCanvas.activeSelf) {
            // programmingEnv.SetActive(false);
            blocksSelectionCanvas.SetActive(false);
            // blocksSelectionCanvas.GetComponent<CanvasGroup>().alpha = 0
        } else {
            // programmingEnv.SetActive(true);
            blocksSelectionCanvas.SetActive(true);
        }
    }
}
