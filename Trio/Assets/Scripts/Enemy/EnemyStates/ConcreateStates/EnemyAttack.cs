using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : EnemyStates
{
    public EnemyAttack(EnemyBase enemyBase, EnemyStateMachine enemyStateMachine) : base(enemyBase, enemyStateMachine)
    {

    }

    public override void AnimationTriggerEvents(EnemyBase.AnimTriggers triggers)
    {
        base.AnimationTriggerEvents(triggers);
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("EnteredAttack");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (enemyBase.isTriggered)
        {
            //enemyStateMachine.ChangeState(enemyBase.enemyAttack);
            //Debug.Log("Attack");
            enemyBase.Move(Vector2.zero);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
