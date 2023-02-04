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

    public override void OnEnterState(params object[] objects)
    {
        controller.StartCoroutine(controller.DisableObject());
    }

    public override void ExecuteState()
    {

    }

    public override void OnExitState()
    {

    }
}