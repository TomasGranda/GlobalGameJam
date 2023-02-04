using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerState : BaseStateMachineState
{
    private readonly Entity controller;

    public FollowPlayerState(Entity controller)
    {
        this.controller = controller;
    }

    public override void OnEnterState(params object[] objects)
    {
        throw new System.NotImplementedException();
    }

    public override void ExecuteState()
    {
        throw new System.NotImplementedException();
    }

    public override void OnExitState()
    {
        throw new System.NotImplementedException();
    }
}