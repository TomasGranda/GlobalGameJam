using UnityEngine;

public class ClimbCliffCommand : Command
{
    private readonly PlayerController controller;
    private readonly Transform transform;

    public ClimbCliffCommand(PlayerController controller)
    {
        this.controller = controller;
        this.transform = controller.transform;
    }

    public void Execute()
    {
        transform.position += transform.up * 2 + transform.right * 2;
    }

    public void Reset()
    {
    }

    public bool ShouldExecute()
    {
        return controller.GetClimbCliffButtonDown() && controller.isOnCliff;
    }
}