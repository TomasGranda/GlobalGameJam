using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : BaseStateMachineState
{
    private readonly Enemy controller;

    private float timeSearch = 50;

    private float currentTime;

    private Vector3 target;

    private Vector3 newPosition;

    public SearchState(Enemy controller)
    {
        this.controller = controller;
    }

    public override void OnEnterState(params object[] objects)
    {
        target = (Vector3)objects[0];

        controller.agent.isStopped = true;
        controller.agent.isStopped = false;

        currentTime = timeSearch;

        newPosition = NewPosition();
    }

    public override void ExecuteState()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            controller.stateMachine.Transition<PatrolState>();
        }

        if (Vector3.Distance(newPosition, controller.transform.position) < 1f)
        {
            newPosition = NewPosition();
        }

        // controller.agent.SetDestination(newPosition);

        controller.RotateTarget(newPosition, controller.stats.maxRotateSpeed);

        controller.FollowTarget(newPosition, controller.stats.moveSpeed);
    }

    public override void OnExitState()
    {
        controller.agent.isStopped = false;
    }

    private float minRange = 5;
    private float maxRange = 10;

    private Vector3 NewPosition()
    {
        var xPos = Random.value * 100;
        var zPos = Random.value * 100;

        float ranPositionX;
        float ranPositionZ;

        if (xPos > 50)
        {
            ranPositionX = Random.Range(target.x + minRange, target.x + maxRange);
        }
        else
        {
            ranPositionX = Random.Range(target.x - minRange, target.x - maxRange);
        }

        if (zPos < 50)
        {
            ranPositionZ = Random.Range(target.z + minRange, target.z + maxRange);
        }
        else
        {
            ranPositionZ = Random.Range(target.z - minRange, target.z - maxRange);
        }

        return new Vector3(ranPositionX, controller.transform.position.y, ranPositionZ);
    }
}