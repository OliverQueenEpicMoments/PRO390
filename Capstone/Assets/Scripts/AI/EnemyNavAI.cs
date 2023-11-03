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
    [SerializeField] GameObject Player;
    [SerializeField] float EnemyDamage = 1;
    [SerializeField] float EnemyKnockback = 0;

    NavMeshAgent Agent;
    Rigidbody2D RB;
    Vector2 Velocity = Vector2.zero;
    private bool AttackSwap = true;
    private bool IsStunned = false;

    void Start() {
        RB = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
        EnemyHealth = GetComponent<Health>();

        Agent = GetComponent<NavMeshAgent>();
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
    }

    void Update() {
        Vector2 Direction = Player.transform.position - transform.position;
        Direction.Normalize();
        Velocity = Direction * Speed;

        if (EnemyHealth.CurrentHealth <= 0) {
            StartCoroutine(Death());
            Agent.SetDestination(transform.position);
        }

        Debug.Log(Velocity.magnitude);
        if (IsStunned) return;

        Agent.SetDestination(Player.transform.position);

        if (EnemyHealth.CurrentHealth > 0) Agent.SetDestination(Player.transform.position);

        //Velocity.x = Direction.x * Speed;
        //Velocity.y = Direction.y * Speed;

        float Distance = (Player.transform.position - transform.position).magnitude;
        if (Distance <= 4 && !IsStunned) {
            StartCoroutine(Attack(AttackSwap));
            StartCoroutine(Stun(0));
        }


        // Move character
        //RB.velocity = Velocity;

        // Update the animator
        animator.SetFloat("Horizontal", Direction.x);
        animator.SetFloat("Vertical", Direction.y);
        animator.SetFloat("Speed", Velocity.magnitude);
    }

    IEnumerator Attack(bool chain) {
        Vector2 Direction = (Player.transform.position - transform.position).normalized;
        Vector2 Knockback = Direction * EnemyKnockback;

        if (chain) {
            animator.SetTrigger("Attack1");
            yield return new WaitForSeconds(0.5f);
            Player.GetComponent<Health>().TakeDamage(EnemyDamage, Knockback);
            AttackSwap = false;
            StartCoroutine(Stun(5));
        } else {
            animator.SetTrigger("Attack2");
            yield return new WaitForSeconds(2f);
            Player.GetComponent<Health>().TakeDamage(EnemyDamage, Knockback);
            AttackSwap = true;
            StartCoroutine(Stun(5));
        }
    }

    IEnumerator Death() {
        Agent.enabled = false;
        yield return new WaitForSeconds(3.75f);
        Destroy(gameObject);
    }

    IEnumerator Stun(float duration) {
        IsStunned = true;
        yield return new WaitForSeconds(duration);
        IsStunned = false;
    }
}