using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSoundState : BaseStateMachineState
{
    private readonly Enemy controller;

    private Vector3 target;

    public FollowSoundState(Enemy controller)
    {
        this.controller = controller;
    }

    public override void OnEnterState(params object[] objects)
    {
        target = (Vector3)objects[0];
    }

    public override void ExecuteState()
    {
        if (controller.isOnVision())
        {
            controller.stateMachine.Transition<FollowPlayerState>();
        }

        if (Vector3.Distance(controller.transform.position, target) > controller.stats.searchRange)
        {
            controller.RotateTargetPlayer();
            controller.FollowTarget(target);
        }
        else
        {
            controller.stateMachine.Transition<SearchState>();
        }
    }

    public override void OnExitState()
    {

    }
}