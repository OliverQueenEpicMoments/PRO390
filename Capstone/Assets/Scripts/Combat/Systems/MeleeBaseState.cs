using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBaseState : State {
    public float Duration;
    public Animator animator;
    protected bool ShouldCombo;
    protected float Power;
    protected float Damage;
    protected AudioClip Attack1Sound;
    protected AudioClip Attack2Sound;
    protected AudioClip Attack3Sound;
    protected Vector3 PlayerPosition;
    protected Vector3 MousePosition;
    protected Vector3 MouseDirection;

    protected Collider2D HitCollider;
    private List<Collider2D> CollidersDamaged;
    private GameObject HitEffectPrefab;
    private float AttackPressedTimer = 0;

    public override void OnEnter(StateMachine stateMachine) {
        base.OnEnter(stateMachine);
        animator = GetComponent<Animator>();
        CollidersDamaged = new List<Collider2D>();
        Power = GetComponent<ComboCharacter>().Power;
        HitCollider = GetComponent<ComboCharacter>().Hitbox;
        PlayerPosition = GetComponent<ComboCharacter>().transform.position;
        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MouseDirection = (MousePosition - PlayerPosition).normalized;
    }

    public override void OnUpdate() {
        base.OnUpdate();
        AttackPressedTimer -= Time.deltaTime;

        if (animator.GetFloat("Weapon.Active") > 0) Attack();
        if (Input.GetMouseButtonDown(0)) AttackPressedTimer = 2.5f;
        if (animator.GetFloat("AttackWindow.Open") > 0f && AttackPressedTimer > 0) ShouldCombo = true;
    }

    public override void OnExit() {
        base.OnExit();
    }

    protected void Attack() {
        //SoundManager.Instance.PlaySound(AttackSound);
        Collider2D[] CollidersToDamage = new Collider2D[10];
        ContactFilter2D Filter = new() {
            useTriggers = true
        };

        int ColliderCount = Physics2D.OverlapCollider(HitCollider, Filter, CollidersToDamage);

        for (int i = 0; i < ColliderCount; i++) {
            if (!CollidersDamaged.Contains(CollidersToDamage[i])) { 
                TeamComponent HitTeamComponent = CollidersToDamage[i].GetComponentInChildren<TeamComponent>();

                if (HitTeamComponent && HitTeamComponent.TeamIndex == TeamIndex.Enemy) {
                    if (HitEffectPrefab != null) Object.Instantiate(HitEffectPrefab, CollidersToDamage[i].transform);
                    HitTeamComponent.GetComponent<Health>().TakeTrueDamage(Damage);
                    Debug.Log(Damage + " true damage dealt");
                    CollidersDamaged.Add(CollidersToDamage[i]);
                }
            }
        }
    }
}