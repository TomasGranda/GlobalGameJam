using UnityEngine;

public class JumpCommand : Command
{
    private readonly PlayerController controller;
    private readonly PlayerModel model;
    private readonly Transform transform;
    private readonly CharacterController characterController;



    public JumpCommand(PlayerController controller)
    {
        this.controller = controller;
        this.model = controller.model;
        this.transform = controller.transform;
        this.characterController = controller.characterController;
    }

    public void Execute()
    {
        if (controller.GetJumpButtonDown() && controller.isOnFloor)
        {
            controller.view.SetAnimationJump(true);
        }
    }

    public void Reset()
    {
        controller.view.SetAnimationJump(false);
    }

    public bool ShouldExecute()
    {
        return controller.GetJumpButtonDown() && controller.isOnFloor && !controller.isClimbing;
    }
}