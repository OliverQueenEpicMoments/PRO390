using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboState : MeleeBaseState {
    public override void OnEnter(StateMachine stateMachine) {
        base.OnEnter(stateMachine);

        // Attack
        AttackIndex = 2;
        Duration = 1.5f;
        animator.SetTrigger("Attack" + AttackIndex);
        animator.SetFloat("MouseXPos", MouseDirection.x);
        animator.SetFloat("MouseYPos", MouseDirection.y);
        //SoundManager.Instance.PlaySound(AttackSound);
    }

    public override void OnUpdate() {
        base.OnUpdate();

        if (FixedTimer >= Duration) {
            if (ShouldCombo) statemachine.SetNextState(new FinisherState());
            else statemachine.SetNextStateToMain();
        }
    }
}