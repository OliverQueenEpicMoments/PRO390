using UnityEngine;
using UnityEngine.InputSystem;

public class ComboCharacter : MonoBehaviour {
    private StateMachine MeleeStateMachine;
    private Abilities AbilityStatus;

    public float Power = 25;
    public Collider2D Hitbox;

    void Start() {
        MeleeStateMachine = GetComponent<StateMachine>();
        AbilityStatus = GetComponent<Abilities>();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0) && MeleeStateMachine.CurrentState.GetType() == typeof(IdleCombatState)) {
            //SoundManager.Instance.PlaySound(GameAssets.Instance.ItemBought);
            MeleeStateMachine.SetNextState(new EntryState());
        }

        if (Input.GetMouseButtonDown(0) && AbilityStatus.EmpoweredAuto && MeleeStateMachine.CurrentState.GetType() != typeof(EmpoweredEntryState)) {
            //SoundManager.Instance.PlaySound(GameAssets.Instance.ItemBought);
            MeleeStateMachine.SetNextState(new EmpoweredEntryState());
        }
    }
}