using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemyStates CurreState {  get; set; }
    public void Initialized(EnemyStates startingState)
    {
        CurreState = startingState;
        CurreState.EnterState();
    }
    public void ChangeState(EnemyStates newState)
    {
        CurreState.ExitState();
        CurreState = newState;
        CurreState.EnterState();
    }
}
