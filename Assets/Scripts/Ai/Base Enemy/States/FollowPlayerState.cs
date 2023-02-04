using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerState : BaseStateMachineState
{
    private readonly Enemy controller;

    public FollowPlayerState(Enemy controller)
    {
        this.controller = controller;
    }

    public override void OnEnterState(params object[] objects)
    {
        controller.agent.stoppingDistance = controller.stats.rangeAttack;
    }

    public override void ExecuteState()
    {
        controller.FollowTarget(controller.playerTarget.position);

        controller.RotateTargetPlayer();

        if (Vector3.Distance(controller.transform.position, controller.playerTarget.position) < controller.stats.rangeAttack && controller.isOnVision())
        {
            controller.stateMachine.Transition<AttackState>();
        }
    }

    public override void OnExitState()
    {

    }
}