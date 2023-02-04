using UnityEngine;

public class MoveCommand : Command
{
    private readonly PlayerController controller;
    private readonly Transform transform;

    public MoveCommand(PlayerController controller)
    {
        this.controller = controller;
        this.transform = controller.transform;
    }

    public void Execute()
    {
        var movement = controller.GetMovementVector();
        movement.z = 0;

        controller.characterController.Move(movement * controller.model.speed * Time.deltaTime);
    }

    public void Reset()
    {
    }

    public bool ShouldExecute()
    {
        return controller.GetMovementVector().magnitude > 0 && !controller.IsOnClimb();
    }
}