using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Error : MonoBehaviour
{
    public TMP_Text errorText;
    public string errorMessage;
    public float errorDuration;
    float counter;
    bool isErrorEmpty = true;

    // fade variables
    float fadeTime;
    bool fadingIn;

    void Start() {
        counter = errorDuration;
        errorText.CrossFadeAlpha(0, 0.0f, false);
    }

    void Update()
    {
        if (!string.IsNullOrEmpty(errorMessage) && isErrorEmpty) {
            // Debug.Log("Enabled");
            fadingIn = true;
            isErrorEmpty = false;
            errorText.text = errorMessage;
            counter = errorDuration;
            errorText.enabled = true;
        }
        if (counter >= 0) {
            // Debug.Log("Active");
            counter -= Time.deltaTime;
        } 
        else if (!isErrorEmpty) {
            // Debug.Log("Disabled");
            isErrorEmpty = true;
            errorMessage = "";
            errorText.text = errorMessage;
            counter = errorDuration;
            errorText.enabled = false;
        }

        if (fadingIn) {
            FadeIn();
        } else if (errorText.color.a != 0) {
            errorText.CrossFadeAlpha(0, 0.5f, false);
        }
    }

    void FadeIn() {
        errorText.CrossFadeAlpha(1, 0.5f, false);
        fadeTime += Time.deltaTime;
        if (errorText.color.a == 1 && fadeTime > 2.5f) {
            fadingIn = false;
            fadeTime = 0;
        }
    }

    // void OnTriggerEnter(Collider other) {
    //     if (other.tag == "Region") {
    //         fadingIn = true;
    //     }
    // }
}
