using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            Agent.SetDestination(transform.position);
            StartCoroutine(Death());
        }

        if (IsStunned) return;

        Agent.SetDestination(Player.transform.position);

        if (EnemyHealth.CurrentHealth > 0) Agent.SetDestination(Player.transform.position);

        //Velocity.x = Direction.x * Speed;
        //Velocity.y = Direction.y * Speed;

        float Distance = (Player.transform.position - transform.position).magnitude;
        if (Distance <= 4.5f && !IsStunned) {
            StartCoroutine(Attack(AttackSwap));
            StartCoroutine(Stun(1));
        }


        // Move character
        //RB.velocity = Velocity;

        // Update the animator
        animator.SetFloat("Horizontal", Direction.x);
        animator.SetFloat("Vertical", Direction.y);
        animator.SetFloat("Speed", Velocity.magnitude);
    }

    IEnumerator Attack(bool chain) {
        if (chain) {
            animator.SetTrigger("Attack1");
            yield return new WaitForSeconds(0.75f);
            AttackSwap = false;
            StartCoroutine(Stun(0.5f));
        } else {
            animator.SetTrigger("Attack2");
            yield return new WaitForSeconds(0.75f);
            AttackSwap = true;
            StartCoroutine(Stun(0.5f));
        }
    }

    IEnumerator Death() {
        IsStunned =  true;
        Agent.enabled = false;
        yield return new WaitForSeconds(3.75f);

        int goldAmount = UnityEngine.Random.Range(10, 20);
        //DamagePopup.Create(GetPosition(), goldAmount, true);
        PlayerController.Instance.AddGoldAmount(goldAmount);

        Destroy(gameObject);
    }

    IEnumerator Stun(float duration) {
        IsStunned = true;
        yield return new WaitForSeconds(duration);
        IsStunned = false;
    }

    public void DamagePlayer(float damagemultiplier) {
        float Distance = (Player.transform.position - transform.position).magnitude;
        Vector2 Direction = (Player.transform.position - transform.position).normalized;
        Vector2 Knockback = Direction * EnemyKnockback;

        if (Distance <= 4.5f) Player.GetComponent<Health>().TakeTrueDamage(EnemyDamage * damagemultiplier, Knockback);
    }
}