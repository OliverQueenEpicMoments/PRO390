using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    [Header("Health")]
    [SerializeField] private float StartingHealth;
    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }
    [SerializeField] private GameObject GoreEffect;

    [Header("IFrames")]
    [SerializeField] private float IFrameDuration;
    [SerializeField] private int NumberOfFlashes;
    private SpriteRenderer spriterenderer;

    [Header("Components")]
    [SerializeField] private Behaviour[] Components;
    public bool Invulnerable = false;

    [Header("Audio")]
    [SerializeField] private AudioClip HurtSound;
    [SerializeField] private AudioClip DeathSound;
    [SerializeField] private AudioClip HealSound;

    private Animator animator;
    private Rigidbody2D RB;

    public void Awake() {
        animator = GetComponent<Animator>();
        spriterenderer = GetComponent<SpriteRenderer>();
        RB = GetComponent<Rigidbody2D>();
        MaxHealth = StartingHealth;
        CurrentHealth = StartingHealth;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.F)) TakeDamage(10);
    }

    public void TakeDamage(float damage) {
        if (Invulnerable) return;
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, MaxHealth);

        if (CurrentHealth > 0) {
            animator.SetTrigger("IsHit");
            StartCoroutine(Invulnerability());
            SoundManager.Instance.PlaySound(HurtSound);
        } else {
            foreach (Behaviour component in Components) component.enabled = false;

            Instantiate(GoreEffect, transform.position, Quaternion.identity);
            animator.SetTrigger("Death");
            SoundManager.Instance.PlaySound(DeathSound);
        }
    }

    public void TakeDamage(float damage, Vector2 knockback) {
        if (Invulnerable) return;
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, MaxHealth);

        if (CurrentHealth > 0) {
            animator.SetTrigger("IsHit");
            RB.AddForce(knockback, ForceMode2D.Impulse);
            StartCoroutine(Invulnerability());
            SoundManager.Instance.PlaySound(HurtSound);
        } else {
            foreach (Behaviour component in Components) component.enabled = false;

            animator.SetTrigger("Death");
            SoundManager.Instance.PlaySound(DeathSound);
        }
    }

    public void AddHealth(float heal) {
        CurrentHealth = Mathf.Clamp(CurrentHealth + heal, 0, MaxHealth);
        SoundManager.Instance.PlaySound(HealSound);
    }

    public void AddMaxHealth(float health) {
        MaxHealth += health;
    }

    public void Respawn() {
        AddHealth(StartingHealth);
        animator.ResetTrigger("Death");
        animator.Play("KnightIdle");
        StartCoroutine(Invulnerability());
        foreach (Behaviour component in Components) component.enabled = true;
    }

    private IEnumerator Invulnerability() {
        Invulnerable = true;
        Physics2D.IgnoreLayerCollision(3, 6, true);
        for (int i = 0; i < NumberOfFlashes; i++) {
            spriterenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(IFrameDuration / (NumberOfFlashes * 2));
            spriterenderer.color = Color.white;
            yield return new WaitForSeconds(IFrameDuration / (NumberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(3, 6, false);
        Invulnerable = false;
    }
}