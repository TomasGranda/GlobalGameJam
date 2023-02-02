using UnityEngine;

public class LeaveCliffCommand : Command
{
    private readonly PlayerController controller;
    private readonly Transform transform;

    public LeaveCliffCommand(PlayerController controller)
    {
        this.controller = controller;
        this.transform = controller.transform;
    }

    public void Execute()
    {
        // controller.characterController.Move(Vector3.left * .1f);
    }

    public void Reset()
    {
    }

    public bool ShouldExecute()
    {
        return controller.GetDownButtonDown() && controller.isOnCliff;
    }
}