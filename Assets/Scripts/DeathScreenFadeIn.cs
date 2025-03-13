using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeathScreenFadeIn : MonoBehaviour
{
    public bool death = false;
    public TMP_Text billyKilledText;
    public Image buttonImage;
    public TMP_Text restartText;

    float alpha1, alpha2, alpha3 = 0;
    float counter = 0;

    void Update() {
        if (death) {
            if (counter < 9f) {
                counter += Time.deltaTime;
            }
            if (counter > 1f && counter < 2f && alpha1 < 254) {
                alpha1 += Time.deltaTime;
                billyKilledText.color = new Color(255f, 0, 0, alpha1);
            }
            if (counter > 2f && counter < 2.3f) {
                if (alpha2 < 128) {
                    alpha2 += 0.01f;
                    Color col = buttonImage.color;
                    col.a = alpha2;
                    buttonImage.color = col;
                }
            }
            if (counter > 2f && alpha3 < 254) {
                alpha3 += Time.deltaTime;
                restartText.color = new Color(255f, 255f, 255f, alpha3);
            }
        }
    }
}
