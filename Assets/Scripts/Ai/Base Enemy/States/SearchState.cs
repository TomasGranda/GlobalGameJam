using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : BaseStateMachineState
{
    private readonly Enemy controller;

    public SearchState(Enemy controller)
    {
        this.controller = controller;
    }

    public override void OnEnterState(params object[] objects)
    {
        controller.agent.isStopped = true;
    }

    public override void ExecuteState()
    {

    }

    public override void OnExitState()
    {

    }
}