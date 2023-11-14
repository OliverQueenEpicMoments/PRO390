using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinisherState : MeleeBaseState {
    public override void OnEnter(StateMachine stateMachine) {
        base.OnEnter(stateMachine);

        // Attack
        Damage = Power * 3;
        Duration = 1.7f;
        animator.SetTrigger("Attack3");
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
