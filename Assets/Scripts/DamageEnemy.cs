using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collission : MonoBehaviour
{
    public HealthBar healthBar;
    public int maxHealth = 100;
    public int health;
    public GameObject explosion;

    void OnTriggerEnter(Collider other)
    {
        // if (!this.gameObject.name.Contains("Enemy")) {
            // Debug.Log(123);
            if (other.gameObject.tag == "Bullet_D40") {
                TakeDamage(40, other.gameObject);
            } else if (other.gameObject.tag == "Bullet_D30") {
                TakeDamage(30, other.gameObject);
            } else if (other.gameObject.tag == "Bullet_D20") {
                TakeDamage(20, other.gameObject);
            } else if (other.gameObject.tag == "Bullet_D10") {
                TakeDamage(10, other.gameObject);
            }
        // }
    }

    void TakeDamage(int damage, GameObject other) {
        healthBar.canvas.enabled = true;
        Destroy(other.gameObject);
        health -= damage;
        healthBar.SetHealth(health);
        if (health <= 0) {
            StartCoroutine(ExplosionAnimation(2f));
            Destroy(this.gameObject.transform.parent.gameObject);
        }
    }

    IEnumerator ExplosionAnimation(float seconds) {
        GameObject ex = Instantiate(explosion, explosion.transform.position, Quaternion.identity) as GameObject;
        ex.SetActive(true);
        yield return new WaitForSeconds(seconds);
        Destroy(ex);
    }
}
