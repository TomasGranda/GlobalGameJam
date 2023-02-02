using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSoundState : BaseStateMachineState
{
    private readonly Entity controller;

    public FollowSoundState(Entity controller)
    {
        this.controller = controller;
    }

    public override void OnEnterState(params object[] objects)
    {

    }

    public override void ExecuteState()
    {
        
    }

    public override void OnExitState()
    {

    }
}