using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PatrolState : BaseStateMachineState
{
    private readonly Enemy controller;

    public PatrolState(Enemy controller)
    {
        this.controller = controller;
    }

    public override void OnEnterState(params object[] objects)
    {
        var list = controller.wayPoints.OrderBy(x => Vector3.Distance(controller.transform.position, x.position)).ToList();

        controller.counterIndex = controller.wayPoints.IndexOf(list[0]);
    }

    public override void ExecuteState()
    {
        controller.FollowPath();

        if (controller.isOnVision())
        {
            controller.stateMachine.Transition<FollowPlayerState>();
        }
    }

    public override void OnExitState()
    {

    }
}