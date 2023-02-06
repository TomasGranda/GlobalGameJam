using UnityEngine;

public class MoveCommand : Command
{
    private readonly PlayerController controller;
    private readonly Transform transform;
    private readonly PlayerModel model;
    private float movementAnimationCurrentSpeed;

    public MoveCommand(PlayerController controller)
    {
        this.controller = controller;
        this.transform = controller.transform;
        this.model = controller.model;
    }

    public void Execute()
    {
        var movement = controller.GetMovementVector();
        movement.z = 0;

        float movementMotion = Mathf.Abs(movement.x);
        float movementSpeed = model.speed * (controller.GetRunButton() ? model.runSpeedMultiplier : 1);

        float animationMovementSpeed = movement.x;
        animationMovementSpeed *= controller.GetRunButton() ? 2 : 1;

        // TODO: Remove this hack when level1 terrain is rotated
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "GameScenes")
            movement.x *= -1;

        movementAnimationCurrentSpeed = Mathf.Abs(Mathf.Lerp(movementAnimationCurrentSpeed, Mathf.Abs(animationMovementSpeed), Time.deltaTime * 3));

        controller.view.SetAnimationMovementSpeed(movementAnimationCurrentSpeed);
        transform.rotation = Quaternion.Euler(0, movement.x > 0 ? 90 : -90, 0);

        controller.characterController.Move(transform.forward * movementMotion * movementSpeed * Time.deltaTime);
    }

    public void Reset()
    {
        movementAnimationCurrentSpeed = Mathf.Abs(Mathf.Lerp(movementAnimationCurrentSpeed, 0, Time.deltaTime * 3));
        if (movementAnimationCurrentSpeed < 0.001f) movementAnimationCurrentSpeed = 0;

        controller.view.SetAnimationMovementSpeed(movementAnimationCurrentSpeed);
    }

    public bool ShouldExecute()
    {
        return controller.GetMovementVector().magnitude > 0 && !controller.IsOnClimb() && controller.canAttack;
    }
}