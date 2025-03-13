using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public TMP_Text healthText;
    public Gradient gradient;
    public Image fill;
    public Canvas canvas;

    void Start() {
        SetMaxHealth((int)slider.maxValue);
    }

    public void SetMaxHealth(int health) {
        slider.maxValue = health;
        slider.value = health;
        healthText.text = $"{health}";
        fill.color = gradient.Evaluate(1f);
        canvas.enabled = false;
    }

    public void SetHealth(int health) {
        slider.value = health;
        healthText.text = $"{health}";
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
