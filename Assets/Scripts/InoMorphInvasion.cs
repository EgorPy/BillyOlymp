using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InoMorphInvasion : MonoBehaviour
{
    public bool isInvading = false;
    bool wasInvading = false;
    public GameObject directionalLight;
    bool lightNeedsToBlink = false;

    // counter for wait variables
    float counter = 0;
    float counterTime = 0.2f;

    // spawning enemies
    public List<GameObject> enemies;
    public Vector3 spawnPosition;
    bool enemyNeedsToBeSpawned = false;
    public GameObject barrier;

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(counter);
        if (counter > 0) {
            counter -= Time.deltaTime;
            return;
        } else if (lightNeedsToBlink) {
            // Debug.Log("enabled");
            counter = 1f;
            lightNeedsToBlink = false;
            directionalLight.SetActive(true);
        } else if (isInvading) {
            SpawnEnemy(1f);
            counter = 0;
        }
        if (isInvading && counter == 0) {
            if (!wasInvading && directionalLight.activeSelf) {
                // Debug.Log(123);
                blinkLight(0.4f);
                wasInvading = true;
            } else {
                blinkLight(0.2f);
                wasInvading = false;
            }
            if (enemyNeedsToBeSpawned && enemies.Count > 0) {
                barrier.SetActive(false);
                enemies[0].SetActive(true);
                enemies[0].transform.position = spawnPosition;
                enemies.RemoveAt(0);
            }
        }
    }

    void blinkLight(float seconds) {
        directionalLight.SetActive(false);
        lightNeedsToBlink = true;
        // Debug.Log("disabled");
        counter = seconds;
    }

    void SpawnEnemy(float seconds) {
        enemyNeedsToBeSpawned = true;
        counter = seconds;
    }

    public void StartInvasion() {
        isInvading = true;
    }

    public void StopInvasion() {
        directionalLight.SetActive(true);
        isInvading = false;
    }

    // public static void RemoveAt<T>(ref T[] arr, int index)
    // {
    //     for (int a = index; a < arr.Length - 1; a++)
    //     {
    //         // moving elements downwards, to fill the gap at [index]
    //         arr[a] = arr[a + 1];
    //     }
    //     // finally, let's decrement Array's size by one
    //     arr.Resize(ref arr, arr.Length - 1);
    // }

}
