using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] SpriteRenderer spriterenderer;
    [SerializeField] float Speed;

    Rigidbody2D RB;

    Vector2 Velocity = Vector2.zero;

    void Start() {
        RB = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        Vector2 Direction = Vector2.zero;
        Direction.x = Input.GetAxis("Horizontal");
        Direction.y = Input.GetAxis("Vertical");

        Debug.DrawRay(transform.position, Direction, Color.green);

        Velocity.x = Direction.x * Speed;
        Velocity.y = Direction.y * Speed;

        // move character
        RB.velocity = Velocity;

        // Rotate character to face direction of movement
        //if (Velocity.x > 0 && !FaceRight) Flip();
        //if (Velocity.x < 0 && FaceRight) Flip();
    }
}