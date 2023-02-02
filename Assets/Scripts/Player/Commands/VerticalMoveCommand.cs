using UnityEngine;

public class VerticalMoveCommand : Command
{
    private readonly PlayerController controller;
    private readonly Transform transform;

    private PlayerModel model;

    public VerticalMoveCommand(PlayerController controller)
    {
        this.controller = controller;
        this.transform = controller.transform;
        this.model = controller.model;
    }

    public void Execute()
    {
        int movement = 0;
        if (controller.GetDownButtonDown()) movement--;
        if (controller.GetUpButtonDown()) movement++;

        Vector3 movementVector = Vector3.forward * movement * controller.model.verticalMovementStepSize;

        if (transform.position.z + movementVector.z > controller.model.maxVerticalSteps * model.verticalMovementStepSize
            || transform.position.z + movementVector.z < 0)
            movementVector.z = 0;

        controller.characterController.Move(movementVector);
    }

    public void Reset()
    {
    }

    public bool ShouldExecute()
    {
        return !controller.IsOnClimb() && (controller.GetDownButtonDown() || controller.GetUpButtonDown());
    }
}