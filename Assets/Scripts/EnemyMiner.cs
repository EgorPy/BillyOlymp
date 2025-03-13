using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class EnemyMiner : MonoBehaviour
{
    public GameObject collectible;
    public NavMeshAgent agent;
    Transform mineral;
    Animator animator;
    public float health;

    // Patroling
    public Vector3 initPosition;
    public Vector3 walkPosition;

    // Mining
    public float miningInterval;
    bool gatheredMineral = false;
    float minDistance;

    // States
    public float sightRange;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update() {
        mineral = FindMineral();
        if (mineral == null) {
            return;
        }
        if (!gatheredMineral) {
            agent.SetDestination(walkPosition);
            animator.SetBool("IsMoving", true);
            if (Mathf.Round(this.gameObject.transform.position.z) == Mathf.Round(walkPosition.z) || Mathf.Round(this.gameObject.transform.position.x) == Mathf.Round(walkPosition.x)) {
                animator.SetBool("IsMining", true);
                animator.SetBool("IsMoving", false);
                transform.LookAt(mineral);
                Invoke(nameof(GatherMineral), miningInterval);
            }
        } else {
            agent.SetDestination(initPosition);
            animator.SetBool("IsMoving", true);
            if (Mathf.Round(this.gameObject.transform.position.z) == Mathf.Round(initPosition.z) || Mathf.Round(this.gameObject.transform.position.x) == Mathf.Round(initPosition.x)) {
                gatheredMineral = false;
                collectible.SetActive(false);
            }
        }
    }

    private Transform FindMineral() {
        minDistance = Mathf.Infinity;
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Mineral");
        foreach(GameObject obj in playerObjects) {
            float distance = Vector3.Distance(this.gameObject.transform.position, obj.GetComponent<Transform>().position);
            if (distance < sightRange) {
                return obj.transform;
            }
        }
        return null;
    }

    private void GatherMineral() {
        collectible.SetActive(true);
        animator.SetBool("IsMining", false);
        gatheredMineral = true;
    }

    private void TakeDamage(int damage) {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), .5f);
    }

    private void DestroyEnemy() {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(initPosition, walkPosition);
    }
}

