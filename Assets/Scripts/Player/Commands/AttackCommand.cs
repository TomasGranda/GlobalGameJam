using UnityEngine;

public class AttackCommand : Command
{
    private readonly PlayerController controller;
    private readonly Transform transform;

    public AttackCommand(PlayerController controller)
    {
        this.controller = controller;
        this.transform = controller.transform;
    }

    public void Execute()
    {
        TargetablePoint target = Utils.GetTargetPoint();
        if (target != null)
        {
            var direction = target.transform.position - transform.position;
            GameObject.Instantiate(controller.model.proyectilePrefab, transform.position, Quaternion.LookRotation(direction.normalized, Vector3.up));
        }
    }

    public void Reset()
    {
    }

    public bool ShouldExecute()
    {
        return controller.GetShootButtonDown();
    }
}