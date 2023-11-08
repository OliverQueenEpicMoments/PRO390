using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] SpriteRenderer spriterenderer;
    [SerializeField] Animator animator;
    [SerializeField] float Speed;

    [Header("Rolling")]
    private bool CanRoll = true;
    private bool IsRolling;
    private bool IsAttacking;
    private bool IsRooted;
    [SerializeField] private float RollingPower = 1.5f;
    [SerializeField] private float RollingTime = 0.8f;
    [SerializeField] private float RollingCooldown = 1.25f;
    [SerializeField] private AudioClip RollSound;
    [SerializeField] private AudioClip Footsteps;

    private Rigidbody2D RB;
    private Health PlayerHealth;
    private Vector2 Velocity = Vector2.zero;
    private Vector3 MousePosition;
    private int EstusFlasks = 3;
    private bool DanceSwap = false;

    void Start() {
        RB = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
        PlayerHealth = GetComponent<Health>();
    }

    void Update() {
        if (IsRolling || IsAttacking) return;
        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector3 MouseDirection = (MousePosition - transform.position).normalized;

        Vector2 Direction = Vector2.zero;
        Direction.x = Input.GetAxis("Horizontal");
        Direction.y = Input.GetAxis("Vertical");
        Direction.Normalize();

        Velocity.x = Direction.x * Speed;
        Velocity.y = Direction.y * Speed;

        // Estus flask chug
        if (Input.GetKeyDown(KeyCode.Q) && EstusFlasks > 0) {
            animator.SetTrigger("IsHealing");
            StartCoroutine(Root(2));
            PlayerHealth.AddHealth(50);
            EstusFlasks--;
        }

        if (IsRooted) return;

        // Rolling
        if (Input.GetKeyDown(KeyCode.LeftShift) && CanRoll) {
            animator.SetTrigger("Roll");
            SoundManager.Instance.PlaySound(RollSound);
            StartCoroutine(Roll());
        }

        // Dance
        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            if (!DanceSwap) {
                animator.SetTrigger("Dance");
                DanceSwap = true;
            } else {
                animator.SetTrigger("Dance2");
                DanceSwap = false;
            }
            //SoundManager.Instance.PlaySound(DanceMusic);
        }

        // Move player
        RB.velocity = Velocity;

        animator.SetFloat("Horizontal", Direction.x);
        animator.SetFloat("Vertical", Direction.y);
        animator.SetFloat("Speed", Direction.sqrMagnitude);

        //animator.SetFloat("MouseXPos", MouseDirection.x);
        //animator.SetFloat("MouseYPos", MouseDirection.y);
    }

    IEnumerator Roll() {
        IsRolling = true;
        PlayerHealth.Invulnerable = true;
        CanRoll = false;
        Physics2D.IgnoreLayerCollision(3, 6, true);
        Velocity = RB.velocity * RollingPower;
        yield return new WaitForSeconds(RollingTime);
        IsRolling = false;
        Physics2D.IgnoreLayerCollision(3, 6, false);
        yield return new WaitForSeconds(RollingCooldown);
        PlayerHealth.Invulnerable = false;
        CanRoll = true;
    }

    IEnumerator Root(float duration) {
        IsRooted = true;
        RB.velocity = Vector2.zero;
        animator.SetFloat("Speed", 0);
        yield return new WaitForSeconds(duration);
        IsRooted = false;
    }
}