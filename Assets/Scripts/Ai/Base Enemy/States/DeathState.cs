using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : BaseStateMachineState
{
    private readonly Enemy controller;

    public DeathState(Enemy controller)
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