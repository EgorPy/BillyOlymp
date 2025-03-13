using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemiesKilledCount : MonoBehaviour
{
    public int enemiesKilled = 0;
    public TMP_Text enemiesKilledText;

    void Update() {
        enemiesKilledText.text = "Врагов уничтожено: " + enemiesKilled.ToString();
    }
}
