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
    //bool FaceRight = true;

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
        if (Input.GetKeyDown(KeyCode.E) && CanRoll) {
            animator.SetTrigger("Roll");
            //SoundManager.Instance.PlaySound(RollSound);
            StartCoroutine(Roll());
        }

        // move character
        RB.velocity = Velocity;

        // Rotate character to face direction of movement
        //if (Velocity.x > 0 && !FaceRight) Flip();
        //if (Velocity.x < 0 && FaceRight) Flip();
    }

    IEnumerator Roll() {
        CanRoll = false;
        IsRolling = true;
        Physics2D.IgnoreLayerCollision(3, 6, true);
        Velocity.x += transform.localScale.x * RollingPower;
        yield return new WaitForSeconds(RollingTime);
        IsRolling = false;
        yield return new WaitForSeconds(RollingCooldown);
        Physics2D.IgnoreLayerCollision(3, 6, false);
        CanRoll = true;
    }
}