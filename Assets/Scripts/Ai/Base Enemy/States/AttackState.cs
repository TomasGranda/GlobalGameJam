using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseStateMachineState
{
    private readonly Enemy controller;

    private bool isExitState;

    public AttackState(Enemy controller)
    {
        this.controller = controller;
    }

    public override void OnEnterState(params object[] objects)
    {
        controller.StartCoroutine(Attack());
    }

    public override void ExecuteState()
    {
        if (controller.playerTarget == null) return;

        controller.RotateTarget(controller.playerTarget.position, controller.stats.maxRotateSpeed);


        if (Vector3.Distance(controller.transform.position, controller.playerTarget.position) > controller.stats.rangeAttack && isExitState)
        {
            controller.stateMachine.Transition<FollowPlayerState>();
        }

        if (isExitState)
        {
            controller.StartCoroutine(Attack());
        }
    }

    public override void OnExitState()
    {
        controller.StopCoroutine(Attack());

        isExitState = true;
    }

    private IEnumerator Attack()
    {
        controller.animator.SetTrigger("Attack");
        isExitState = false;
        yield return new WaitForSeconds(2);
        isExitState = true;
    }
}