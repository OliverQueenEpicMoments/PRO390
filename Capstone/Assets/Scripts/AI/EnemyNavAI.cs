using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavAI : MonoBehaviour {
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriterenderer;
    [SerializeField] Health EnemyHealth;
    [SerializeField] float Speed;
    [Header("AI")]
    [SerializeField] Transform PlayerLocation;
    [SerializeField] float EnemyDamage = 1;
    [SerializeField] float EnemyKnockback = 0;

    NavMeshAgent Agent;
    Rigidbody2D RB;
    Vector2 Velocity = Vector2.zero;

    void Start() {
        RB = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
        EnemyHealth = GetComponent<Health>();

        Agent = GetComponent<NavMeshAgent>();
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
    }

    void Update() {
        if (EnemyHealth.CurrentHealth <= 0) StartCoroutine(Death());

        if (EnemyHealth.CurrentHealth > 0) Agent.SetDestination(PlayerLocation.position);
        Vector2 Direction = PlayerLocation.position - transform.position;

        //Velocity.x = Direction.x * Speed;
        //Velocity.y = Direction.y * Speed;

        float Distance = (PlayerLocation.position - transform.position).magnitude;
        if (Distance <= 4) {
            // Attack
            animator.SetTrigger("Attack1");
        }

        //Direction.Normalize();
        //Velocity = Direction * Speed;

        // Move character
        //RB.velocity = Velocity;

        // Update the animator
        animator.SetFloat("Horizontal", Direction.x);
        animator.SetFloat("Vertical", Direction.y);
        animator.SetFloat("Speed", Direction.sqrMagnitude);
    }

    IEnumerator Death() {
        yield return new WaitForSeconds(3.75f);
        Destroy(gameObject);
    }
}