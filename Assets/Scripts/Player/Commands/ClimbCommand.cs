using UnityEngine;

public class ClimbCommand : Command
{
    private readonly PlayerController controller;
    private readonly Transform transform;

    public ClimbCommand(PlayerController controller)
    {
        this.controller = controller;
        this.transform = controller.transform;
    }

    public void Execute()
    {
        var movement = controller.GetClimbVector();

        controller.characterController.Move(movement * controller.model.speed * Time.deltaTime);
        controller.view.SetClimbSpeedAnimation(movement.y);
    }

    public void Reset()
    {
        controller.view.SetClimbSpeedAnimation(0);
    }

    public bool ShouldExecute()
    {
        return controller.GetMovementVector().magnitude > 0 && controller.isClimbing;
    }
}