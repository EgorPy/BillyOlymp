using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour
{
    public GameObject projectile;
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask groundMask, playerMask;
    // public float acceleration = 2f;
    // public float deceleration = 60f;
    // public float closeEnoughMeters = 4f;
    // public ThirdPersonCharacter character;
    Vector3 projectilePosition;
    Animator animator;
    EnemiesKilledCount ek;

    public int health;
    public int damage;

    // Patroling
    public Vector3 walkPoint;
    public bool walkPointSet = false;
    bool coroutineStarted = false;
    public float walkPointRange;
    Vector3 initPosition;

    // Attacking
    public float attackInterval;
    bool alreadyAttacked;
    float minDistance;
    public bool playerAttacked;
    // float rotation;
    // float currentAngularVelocity;
    // Vector3 lastEulerAngles = new Vector3(0, 0, 0);

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        // projectilePosition = projectile.transform.position;
        animator = GetComponentInChildren<Animator>();
        initPosition = this.gameObject.transform.position;
        player = this.transform;
    }

    void Start() {
        ek = GameObject.FindObjectOfType<EnemiesKilledCount>();
    }

    private void Update() {
        // Check for a sight and attack range

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);

        if (!playerAttacked) {
            FindPlayer();
            if (!playerInSightRange && !playerInAttackRange) Patroling();
        }

        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
        if (playerAttacked) ChasePlayer();


        // if (agent.hasPath)
        //     agent.acceleration = (agent.remainingDistance < closeEnoughMeters) ? deceleration : acceleration;

        // if (agent.remainingDistance > agent.stoppingDistance) {
        //     character.Move(agent.desiredVelocity, false, false);
        // } else {
        //     character.Move(Vector3.zero, false, false);
        // }

        // Debug.Log(lastEulerAngles.y - transform.eulerAngles.y);

        // if (lastEulerAngles.y != transform.eulerAngles.y) {
        //     animator.SetBool("IsMoving", true);
        // } else {
        //     animator.SetBool("IsMoving", false);
        // }

        // lastEulerAngles = transform.eulerAngles;
    }

    private void FindPlayer() {
        minDistance = Mathf.Infinity;
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Selectable");
        foreach(GameObject obj in playerObjects) {
            float distance = Vector3.Distance(this.gameObject.transform.position, obj.GetComponent<Transform>().position);
            if (distance < sightRange) {
                player = obj.transform;
            }
        }
    }

    private void Patroling() {
        if (!walkPointSet && !coroutineStarted) StartCoroutine(SearchForWalkPoint(2f));

        if (walkPointSet) {
            animator.SetBool("IsMoving", true);
            agent.SetDestination(walkPoint);
        }

        // Walkpoint reached
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f) {
            walkPointSet = false;
            animator.SetBool("IsMoving", false);
        }
    }

    private IEnumerator SearchForWalkPoint(float seconds) {
        coroutineStarted = true;
        yield return new WaitForSeconds(seconds);

        // Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(initPosition.x + randomX, transform.position.y, initPosition.z + randomZ);

        // if (Physics.Raycast(walkPoint, -transform.up, 2f, groundMask)) {
        //     // Walkpoint reached
        //     if ((transform.position - walkPoint).magnitude < 1f) {
        //         walkPointSet = false;
        //         animator.SetBool("IsMoving", false);
        //     } else {
        //         walkPointSet = true;
        //     }
        // }
        walkPointSet = true;
        coroutineStarted = false;
    }

    private void ChasePlayer() {
        animator.SetBool("IsMoving", true);
        if (player != null) {
        agent.SetDestination(player.position);
        }
    }

    private void AttackPlayer() {
        animator.SetBool("IsMoving", false);

        // Make sure enemy doesn't move
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked) {
            // Attack code here
            playerAttacked = false;
            animator.SetBool("IsAttacking", true);
            GameObject go = Instantiate(projectile, projectile.transform.position, projectile.transform.rotation);
            go.SetActive(true);
            // go.GetComponent<Transform>().Translate(projectilePosition);
            Rigidbody rb = go.GetComponent<Rigidbody>();
            rb.AddForce(projectile.transform.forward * 96f, ForceMode.Impulse);
            // rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), attackInterval);
            StartCoroutine(DestroyBulletAfterTime(go, attackInterval));
        }
    }

    private void ResetAttack() {
        alreadyAttacked = false;
        animator.SetBool("IsAttacking", false);
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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(initPosition, walkPointRange);
    }

    private IEnumerator DestroyBulletAfterTime(GameObject go, float seconds) {
        yield return new WaitForSeconds(seconds);
        Destroy(go);
    }

    void OnDestroy() {
        if (ek != null) {
            ek.enemiesKilled++;
        }
    }

    public void ChaseNearestPlayerObject() {
        minDistance = Mathf.Infinity;
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Selectable");
        foreach(GameObject obj in playerObjects) {
            float distance = Vector3.Distance(this.gameObject.transform.position, obj.GetComponent<Transform>().position);
            if (distance < minDistance) {
                player = obj.transform;
            }
        }
        playerAttacked = true;
    }
}
