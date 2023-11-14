using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryState : MeleeBaseState {
    public override void OnEnter(StateMachine stateMachine) {
        base.OnEnter(stateMachine);

        // Attack
        Damage = Power;
        Duration = 1f;
        animator.SetTrigger("Attack1");
        animator.SetFloat("MouseXPos", MouseDirection.x);
        animator.SetFloat("MouseYPos", MouseDirection.y);
        //SoundManager.Instance.PlaySound(AttackSound);
    }

    public override void OnUpdate() {
        base.OnUpdate();

        if (FixedTimer >= Duration) {
            if (ShouldCombo) statemachine.SetNextState(new ComboState());
            else statemachine.SetNextStateToMain();
        }
    }
}