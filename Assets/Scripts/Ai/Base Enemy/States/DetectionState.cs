using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionState : BaseStateMachineState
{
    private readonly Entity controller;

    public DetectionState(Entity controller)
    {
        this.controller = controller;
    }

    public override void ExecuteState()
    {
        throw new System.NotImplementedException();
    }

    public override void OnEnterState(params object[] objects)
    {
        throw new System.NotImplementedException();
    }

    public override void OnExitState()
    {
        throw new System.NotImplementedException();
    }
}