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
    [SerializeField] private float RollingPower = 1.5f;
    [SerializeField] private float RollingTime = 0.8f;
    [SerializeField] private float RollingCooldown = 1.25f;

    Rigidbody2D RB;

    Vector2 Velocity = Vector2.zero;
    bool FaceRight = true;

    void Start() {
        RB = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        if (IsRolling) return;

        Vector2 Direction = Vector2.zero;
        Direction.x = Input.GetAxis("Horizontal");
        Direction.y = Input.GetAxis("Vertical");
        Direction.Normalize();

        Velocity.x = Direction.x * Speed;
        Velocity.y = Direction.y * Speed;

        // Rolling
        if (Input.GetKeyDown(KeyCode.LeftShift) && CanRoll) {
            Debug.Log("Rolling");
            //animator.SetTrigger("Roll");
            //SoundManager.Instance.PlaySound(RollSound);
            StartCoroutine(Roll());
        }

        // Move player
        RB.velocity = Velocity;

        // Rotate character to face direction of movement
        if (Velocity.x > 0 && !FaceRight) Flip();
        if (Velocity.x < 0 && FaceRight) Flip();

        animator.SetFloat("Horizontal", Direction.x);
        animator.SetFloat("Vertical", Direction.y);
        animator.SetFloat("Speed", Direction.sqrMagnitude);
    }

    IEnumerator Roll() {
        CanRoll = false;
        IsRolling = true;
        Physics2D.IgnoreLayerCollision(3, 6, true);
        Velocity = RB.velocity * RollingPower;
        //Velocity.y += transform.localScale.y * RollingPower;
        yield return new WaitForSeconds(RollingTime);
        IsRolling = false;
        yield return new WaitForSeconds(RollingCooldown);
        Physics2D.IgnoreLayerCollision(3, 6, false);
        CanRoll = true;
    }

    private void Flip() {
        Vector3 CurrentScale = gameObject.transform.localScale;
        CurrentScale.x *= -1;
        gameObject.transform.localScale = CurrentScale;

        FaceRight = !FaceRight;
    }
}