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
        Waiting();
    }

    public override void OnExitState()
    {

    }

    private void Waiting()
    {
        if (!controller.isWaiting) return;

        counterTime -= Time.deltaTime;

        if (counterTime <= 0)
        {
            controller.wayPoints.Reverse();

            // controller.transform.rotation = Quaternion.AngleAxis(0, controller.wayPoints[1].transform.position - controller.transform.position);

            // var angle = Vector3.Angle(controller.wayPoints[1].transform.position - controller.transform.position, controller.transform.forward);

            // Debug.Log(angle);

            // if (angle <= 1)
            // {
                controller.isWaiting = false;

                controller.stateMachine.Transition<PatrolState>();
            // }
        }
    }

    private void ChangeViewPlayer()
    {
        if (controller.isOnVision())
        {
            controller.stateMachine.Transition<FollowPlayerState>();
        }
    }

    private void ChangeDetectionSound()
    {
        if (controller.isDetectedSound)
        {
            controller.stateMachine.Transition<FollowSoundState>();
        }
    }
}