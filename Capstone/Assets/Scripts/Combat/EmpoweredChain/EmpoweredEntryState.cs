using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpoweredEntryState : MeleeBaseState {
    public override void OnEnter(StateMachine stateMachine) {
        base.OnEnter(stateMachine);

        // Attack
        AttackIndex = 2;
        Duration = 2f;
        animator.SetTrigger("EmpoweredAttack" + 1);
        animator.SetFloat("MouseXPos", MouseDirection.x);
        animator.SetFloat("MouseYPos", MouseDirection.y);
        //SoundManager.Instance.PlaySound(Attack1Sound);
    }

    public override void OnUpdate() {
        base.OnUpdate();

        if (FixedTimer >= Duration) {
            if (ShouldCombo) statemachine.SetNextState(new EmpoweredFinisherState());
            else statemachine.SetNextStateToMain();
        }
    }
}