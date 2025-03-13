using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowMinerals : MonoBehaviour
{
    public int mineralsCount;
    public TMP_Text MineralsText;

    void Start()
    {
        PlayerPrefs.SetInt("mineralsCount", mineralsCount);
    }

    void Update()
    {
        mineralsCount = PlayerPrefs.GetInt("mineralsCount");
        MineralsText.text = $"<sprite name=\"Minerals\">{mineralsCount}";
    }
}
