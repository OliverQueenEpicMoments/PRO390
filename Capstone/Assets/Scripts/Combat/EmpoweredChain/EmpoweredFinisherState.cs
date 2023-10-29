using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpoweredFinisherState : MeleeBaseState {
    public override void OnEnter(StateMachine stateMachine) {
        base.OnEnter(stateMachine);

        // Attack
        AttackIndex = 3;
        Duration = 1f;
        animator.SetTrigger("EmpoweredAttack" + 2);
        animator.SetFloat("MouseXPos", MouseDirection.x);
        animator.SetFloat("MouseYPos", MouseDirection.y);
        //SoundManager.Instance.PlaySound(AttackSound);
    }

    public override void OnUpdate() {
        base.OnUpdate();

        if (FixedTimer >= Duration) {
            statemachine.SetNextStateToMain();
        }
    }
}