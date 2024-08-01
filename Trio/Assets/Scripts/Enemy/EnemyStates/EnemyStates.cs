using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStates
{
    protected EnemyBase enemyBase;
    protected EnemyStateMachine enemyStateMachine;

    public EnemyStates(EnemyBase enemyBase, EnemyStateMachine enemyStateMachine)
    {
        this.enemyBase = enemyBase;
        this.enemyStateMachine = enemyStateMachine;
    }
    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void FrameUpdate() { }
    public virtual void PhysicsUpdate() { }
    public virtual void AnimationTriggerEvents(EnemyBase.AnimTriggers triggers) { }
}
