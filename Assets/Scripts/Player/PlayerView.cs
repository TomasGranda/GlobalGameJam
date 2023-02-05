using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerView
{
    public Animator animator;

    public string movementSpeedAnimationParam;

    public void SetAnimationMovementSpeed(float speed)
    {
        animator.SetFloat(movementSpeedAnimationParam, speed);
    }

    public void SetAnimationVerticalSpeed(float speed)
    {
        animator.SetFloat("VerticalSpeed", speed);
    }

    public void SetAnimationJump(bool state)
    {
        animator.SetBool("Jumping", state);
    }

    public void SetAnimationasd()
    {
        animator.SetTrigger("asd");
    }

    public void SetAnimationOnFloor(bool state)
    {
        animator.SetBool("OnFloor", state);
    }

    public void SetAnimationThrow()
    {
        animator.SetTrigger("Throw");
    }

    public void SetClimbAnimation(bool state)
    {
        animator.SetBool("Climbing", state);
    }

    public void SetClimbSpeedAnimation(float speed)
    {
        animator.SetFloat("ClimbingSpeed", speed);
    }

    public void SetOnCliffAnimation(bool state)
    {
        animator.SetBool("OnCliff", state);
    }

    public void SetCliffUpAnimation()
    {
        animator.SetTrigger("CliffUp");
    }

}
