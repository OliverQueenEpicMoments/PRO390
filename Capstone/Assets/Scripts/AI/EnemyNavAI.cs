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
    bool FaceRight = true;

    void Start() {
        RB = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
        EnemyHealth = GetComponent<Health>();

        Agent = GetComponent<NavMeshAgent>();
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
    }

    void Update() {
        Agent.SetDestination(PlayerLocation.position);
        //Vector2 Direction = PlayerLocation.position - transform.position;

        float Distance = (PlayerLocation.position - transform.position).magnitude;
        if (Distance <= 2) {
            // Attack
            //Debug.Log("Attack");
        }

        //Direction.Normalize();
        //Velocity = Direction * Speed;

        // Move character
        RB.velocity = Velocity;

        // Rotate character to face direction of movement
        if (Velocity.x > 0 && !FaceRight) Flip();
        if (Velocity.x < 0 && FaceRight) Flip();

        // Update the animator
        //animator.SetFloat("Speed", Mathf.Abs(Velocity.x));
    }

    private void Flip() {
        Vector3 CurrentScale = gameObject.transform.localScale;
        CurrentScale.x *= -1;
        gameObject.transform.localScale = CurrentScale;

        FaceRight = !FaceRight;
    }
}