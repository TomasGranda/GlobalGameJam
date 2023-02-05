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
}
