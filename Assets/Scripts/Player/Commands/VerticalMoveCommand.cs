using UnityEngine;

public class VerticalMoveCommand : Command
{
    private readonly PlayerController controller;
    private readonly Transform transform;

    private PlayerModel model;

    private float positionIndex = 0;

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

        if (positionIndex + movement > model.maxVerticalSteps) movement = 0;

        positionIndex += movement;

        Vector3 movementVector = Vector3.back * movement * controller.model.verticalMovementStepSize;

        controller.characterController.Move(movementVector);
    }

    public void Reset()
    {
    }

    public bool ShouldExecute()
    {
        return !controller.IsOnClimb() && controller.isOnFloor && (controller.GetDownButtonDown() || controller.GetUpButtonDown());
    }
}