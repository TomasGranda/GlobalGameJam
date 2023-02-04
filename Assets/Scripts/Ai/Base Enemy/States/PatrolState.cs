using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseStateMachineState
{
    private readonly Enemy controller;

    public PatrolState(Enemy controller)
    {
        this.controller = controller;
    }

    public override void OnEnterState(params object[] objects)
    {

    }

    public override void ExecuteState()
    {
        controller.FollowPath();

        if (controller.isOnVision())
        {
            controller.stateMachine.Transition<FollowPlayerState>();
        }
        else
        {

        }
    }

    public override void OnExitState()
    {

    }
}