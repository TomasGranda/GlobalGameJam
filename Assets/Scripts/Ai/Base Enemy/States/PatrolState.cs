using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseStateMachineState
{
    private readonly Entity controller;

    private Vector3 nextPosition;

    public PatrolState(Entity controller)
    {
        this.controller = controller;
    }

    public override void ExecuteState()
    {
        throw new System.NotImplementedException();
    }

    public override void OnEnterState(params object[] objects)
    {
        Debug.Log("asd");
    }

    public override void OnExitState()
    {
        throw new System.NotImplementedException();
    }
}