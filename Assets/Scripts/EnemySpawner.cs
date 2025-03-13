using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemies;
    public float spawnInterval;
    public Vector3 enemiesWalkPoint;
    float counter = 0;
    int index;

    void Start() {
        counter = spawnInterval - 2f;
    }

    void Update()
    {
        counter += Time.deltaTime;
        if (counter > spawnInterval) {
            SpawnEnemy();
            counter = 0;
        }
    }

    void SpawnEnemy() {
        GameObject enemy = Instantiate(enemies[index], this.gameObject.transform.position, this.gameObject.transform.rotation);
        enemy.GetComponent<Enemy>().ChaseNearestPlayerObject();
        if (index < enemies.Length - 1) {
            index++;
        } else {
            index = 0;
        }
    }
}
