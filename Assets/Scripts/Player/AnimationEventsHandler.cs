using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventsHandler : MonoBehaviour
{
    public PlayerController controller;

    public void JumpEvent()
    {
        controller.HandleJumpEvent();
    }

    public void AttackRefreshEvent()
    {
        controller.HandleAttackRefreshEvent();
    }

    public void CliffUpEndsEvent()
    {
        controller.HandleCliffUpEndsEvent();
    }
}
