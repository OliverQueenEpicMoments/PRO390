using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    [SerializeField] private float Size = 1;
    [SerializeField] private float PowerScaling = 1.25f;
    [SerializeField] private GameObject ParticleEffect;
    [SerializeField] private AudioClip ExplosionSound;

    private GameObject Player;
    private CircleCollider2D Collider;
    private float Timer = 3;
    private float Damage;

    void Start() {
        Player = GameObject.FindGameObjectWithTag("Player");
        Collider = GetComponent<CircleCollider2D>();

        transform.localScale *= Size;
        SoundManager.Instance.PlaySound(ExplosionSound);
    }

    void Update() {
        Timer -= Time.deltaTime;

        if (Timer <= 2) Collider.enabled = false;

        if (Timer <= 0) Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Damage = Player.GetComponent<ComboCharacter>().Power * PowerScaling;

        if (collision.CompareTag("Enemy")) {
            collision.GetComponent<Health>().TakeDamage(Damage);
        }
    }
}