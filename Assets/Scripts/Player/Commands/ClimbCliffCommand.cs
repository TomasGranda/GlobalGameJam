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
        controller.cliffAnimation = true;
    }

    public void Reset()
    {
    }

    public bool ShouldExecute()
    {
        return controller.GetUpButtonDown() && controller.isOnCliff;
    }
}