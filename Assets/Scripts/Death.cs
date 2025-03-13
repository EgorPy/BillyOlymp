using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Death : MonoBehaviour
{
    public Volume deathVolume;
    public Volume blurVolume;
    public GameObject deathVolumeParent;
    public GameObject blurVolumeParent;
    public GameObject deathScreen;
    DeathScreenFadeIn ds;
    CameraShake cs;
    public GameObject blocksEngine2;
    public GameObject ui;

    void Start() {
        ds = GameObject.FindObjectOfType<DeathScreenFadeIn>();
        cs = GameObject.FindObjectOfType<CameraShake>();
    }

    void OnDestroy()
    {
        blocksEngine2.SetActive(false);
        ui.SetActive(false);

        deathVolumeParent.SetActive(true);
        blurVolumeParent.SetActive(true);
        deathVolume.weight = 1;
        blurVolume.weight = 1;
        ds.death = true;
        deathScreen.SetActive(true);
    }
}
