using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseStateMachineState
{
    private readonly Entity controller;

    private float timeExitState = 5;
    private float counterTime;

    public IdleState(Entity controller)
    {
        this.controller = controller;
    }

    public override void OnEnterState(params object[] objects)
    {
        counterTime = timeExitState;
    }

    public override void ExecuteState()
    {
        counterTime -= Time.deltaTime;

        if (counterTime <= 0)
        {
            controller.stateMachine.Transition<PatrolState>();
        }
    }

    public override void OnExitState()
    {

    }

    public void ChangeViewPlayer()
    {
        if (controller.isOnVision())
        {
            controller.stateMachine.Transition<FollowPlayerState>();
        }
    }

    public void ChangeDetectionSound()
    {
        if (controller.isDetectedSound)
        {
            controller.stateMachine.Transition<FollowSoundState>();
        }
    }
}