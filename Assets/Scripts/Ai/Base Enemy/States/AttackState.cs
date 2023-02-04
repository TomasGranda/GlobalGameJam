using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseStateMachineState
{
    private readonly Enemy controller;

    public AttackState(Enemy controller)
    {
        this.controller = controller;
    }

    public override void OnEnterState(params object[] objects)
    {

    }

    public override void ExecuteState()
    {
        controller.RotateTarget(controller.playerTarget.position, controller.stats.maxRotateSpeed);

        if (Vector3.Distance(controller.transform.position, controller.playerTarget.position) > controller.stats.rangeAttack)
        {
            controller.stateMachine.Transition<FollowPlayerState>();
        }
    }

    public override void OnExitState()
    {

    }
}