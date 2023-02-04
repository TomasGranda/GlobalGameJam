using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSoundState : BaseStateMachineState
{
    private readonly Entity controller;

    private Vector3 target;

    public FollowSoundState(Entity controller)
    {
        this.controller = controller;
    }

    public override void OnEnterState(params object[] objects)
    {
        target = (Vector3)objects[0];
    }

    public override void ExecuteState()
    {
        controller.agent.SetDestination(target);
    }

    public override void OnExitState()
    {

    }
}