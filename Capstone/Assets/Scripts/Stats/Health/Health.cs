using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    public static event Action OnPlayerDeath;

    [Header("Health")]
    [SerializeField] private float StartingHealth;
    [SerializeField] private float Protections;
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
        if (Input.GetKeyDown(KeyCode.F)) TakeTrueDamage(10);
    }

    public void TakeDamage(float damage) {
        if (Invulnerable) return;
        float MitigatedDamage = 100 * damage / (Protections + 100);
        CurrentHealth = Mathf.Clamp(CurrentHealth - MitigatedDamage, 0, MaxHealth);

        if (CurrentHealth > 0) {
            animator.SetTrigger("IsHit");
            StartCoroutine(Invulnerability());
            SoundManager.Instance.PlaySound(HurtSound);
        } else {
            foreach (Behaviour component in Components) component.enabled = false;

            Instantiate(GoreEffect, transform.position, Quaternion.identity);
            animator.SetTrigger("Death");
            SoundManager.Instance.PlaySound(DeathSound);

            if (gameObject.CompareTag("Player")) {
                OnPlayerDeath?.Invoke();
                Destroy(gameObject);
            }
        }
    }

    public void TakeDamage(float damage, Vector2 knockback) {
        if (Invulnerable) return;
        float MitigatedDamage = 100 * damage / (Protections + 100);
        CurrentHealth = Mathf.Clamp(CurrentHealth - MitigatedDamage, 0, MaxHealth);

        if (CurrentHealth > 0) {
            animator.SetTrigger("IsHit");
            RB.AddForce(knockback, ForceMode2D.Impulse);
            StartCoroutine(Invulnerability());
            SoundManager.Instance.PlaySound(HurtSound);
        } else {
            foreach (Behaviour component in Components) component.enabled = false;

            Instantiate(GoreEffect, transform.position, Quaternion.identity);
            animator.SetTrigger("Death");
            SoundManager.Instance.PlaySound(DeathSound);

            if (gameObject.CompareTag("Player")) {
                OnPlayerDeath?.Invoke();
                Destroy(gameObject);
            }
        }
    }

    public void TakeTrueDamage(float damage) {
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

            if (gameObject.CompareTag("Player")) {
                OnPlayerDeath?.Invoke();
                Destroy(gameObject);
            }
        }
    }

    public void TakeTrueDamage(float damage, Vector2 knockback) {
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

            if (gameObject.CompareTag("Player")) {
                OnPlayerDeath?.Invoke();
                Destroy(gameObject);
            }
        }
    }

    public void Heal(float heal) {
        CurrentHealth = Mathf.Clamp(CurrentHealth + heal, 0, MaxHealth);
        SoundManager.Instance.PlaySound(HealSound);
        //Debug.Log("Healed " + heal);
    }

    public void MaxHealthHeal(float percentage) {
        // Do some lerp stuff here
        float Heal = MaxHealth * percentage / 100;
        CurrentHealth = Mathf.Clamp(CurrentHealth + Heal, 0, MaxHealth);
        SoundManager.Instance.PlaySound(HealSound);
        //Debug.Log("Healed " + percentage + "% which is " + Heal);
    }

    public void AddMaxHealth(float health) {
        MaxHealth += health;
        CurrentHealth = Mathf.Clamp(CurrentHealth + (health / 2), 0, MaxHealth);
    }

    public void AddProtections(float prots) {
        Protections += prots;
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