using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCharacter : MonoBehaviour {
    private StateMachine MeleeStateMachine;
    private Abilities AbilityStatus;

    public AudioClip Attack1Sound;
    public AudioClip Attack2Sound;
    public AudioClip Attack3Sound;
    public AudioClip EmpoweredAttack1Sound;
    public AudioClip EmpoweredAttack2Sound;

    public Collider2D Hitbox;
    public GameObject HitEffect;

    void Start() {
        MeleeStateMachine = GetComponent<StateMachine>();
        AbilityStatus = GetComponent<Abilities>();
    }

    void Update() {
        if (Input.GetMouseButton(0) && MeleeStateMachine.CurrentState.GetType() == typeof(IdleCombatState)) {
            MeleeStateMachine.SetNextState(new EntryState());
        }

        if (Input.GetMouseButton(0) && AbilityStatus.EmpoweredAuto && MeleeStateMachine.CurrentState.GetType() != typeof(EmpoweredEntryState)) {
            MeleeStateMachine.SetNextState(new EmpoweredEntryState());
        }
    }
}