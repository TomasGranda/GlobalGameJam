using UnityEngine;

public class VerticalMoveCommand : Command
{
    private readonly PlayerController controller;
    private readonly Transform transform;

    private PlayerModel model;

    private float initialZPosition;

    public VerticalMoveCommand(PlayerController controller)
    {
        this.controller = controller;
        this.transform = controller.transform;
        this.model = controller.model;
        this.initialZPosition = transform.position.z;
    }

    public void Execute()
    {
        int movement = 0;
        if (controller.GetDownButtonDown()) movement--;
        if (controller.GetUpButtonDown()) movement++;

        Debug.Log(movement);

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