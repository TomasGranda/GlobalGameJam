using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerModel model;

    public PlayerView view;

    public InputActionAsset inputAsset;

    public CharacterController characterController { get; private set; }

    public bool debug;

    public bool isClimbing;

    public bool isOnCliff;

    public bool isOnFloor;

    [HideInInspector]
    public Vector3 moveDirection = Vector3.zero;

    public bool cliffAnimation;

    private Commands commands;

    public bool canAttack = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Commands
        commands = new Commands();

        MoveCommand moveCommand = new MoveCommand(this);
        VerticalMoveCommand verticalMoveCommand = new VerticalMoveCommand(this);
        JumpCommand jumpCommand = new JumpCommand(this);
        ClimbCommand climbCommand = new ClimbCommand(this);
        ClimbCliffCommand climbCliffCommand = new ClimbCliffCommand(this);
        LeaveCliffCommand leaveCliffCommand = new LeaveCliffCommand(this);
        AttackCommand attackCommand = new AttackCommand(this);

        commands.AddCommand(climbCommand);
        commands.AddCommand(climbCliffCommand);
        commands.AddCommand(leaveCliffCommand);
        commands.AddCommand(moveCommand);
        commands.AddCommand(verticalMoveCommand);
        commands.AddCommand(jumpCommand);
        commands.AddCommand(attackCommand);
    }

    // TODO: Borrar
    int counter = 0;

    void Update()
    {
        if (!cliffAnimation)
        {
            commands.ExecuteCommands();

            // Gravity
            if (!IsOnClimb())
            {
                if (!isOnFloor)
                    moveDirection.y -= model.gravityMagnitude * Time.deltaTime;

                characterController.Move(moveDirection * Time.deltaTime);
            }

            CheckWalls();
            CheckIsPlayerOnFloor();
        }
        else
        {
            if (counter <= 2)
                characterController.Move(Vector3.up);

            if (counter > 2 && counter < 4)
                characterController.Move(Vector3.right);

            counter++;

            if (counter >= 4)
            {
                cliffAnimation = false;
                counter = 0;
            }
        }
    }

    #region RaycastCheckers
    public void CheckWalls()
    {
        var newIsClimbing = Physics.Raycast(transform.position, transform.forward, model.wallCheckRaycastLenght, model.climbableWallLayer);

        isOnCliff = Physics.Raycast(transform.position, transform.forward, model.wallCheckRaycastLenght, model.cliffLayer);

        if (isClimbing && isClimbing != newIsClimbing && !isOnCliff)
        {
            characterController.Move(-transform.forward * .1f);
        }
        isClimbing = newIsClimbing;
        view.SetClimbAnimation(isClimbing);
    }

    public bool CheckIsPlayerOnFloor()
    {
        isOnFloor = Physics.Raycast(transform.position, Vector3.down, model.groundCheckRaycastLenght, model.floorMask);
        return isOnFloor;
    }
    #endregion

    #region InputSystem
    public Vector3 GetMovementVector()
    {
        return inputAsset.FindAction("Movement").ReadValue<Vector3>();
    }

    public Vector3 GetClimbVector()
    {
        return inputAsset.FindAction("Climb").ReadValue<Vector3>();
    }

    public bool GetJumpButtonDown()
    {
        return inputAsset.FindAction("Jump").WasPressedThisFrame();
    }

    public bool GetShootButtonDown()
    {
        return inputAsset.FindAction("Shoot").WasPressedThisFrame();
    }

    public bool GetUpButtonDown()
    {
        return inputAsset.FindAction("UpButton").WasPressedThisFrame();
    }

    public bool GetDownButtonDown()
    {
        return inputAsset.FindAction("DownButton").WasPressedThisFrame();
    }
    #endregion

    public bool IsOnClimb()
    {
        return isClimbing || isOnCliff;
    }

    #region AnimationHandlers
    public void HandleJumpEvent()
    {
        moveDirection.y = model.jumpSpeed;
        view.SetAnimationOnFloor(false);
        view.SetAnimationasd();
    }

    public void HandleAttackRefreshEvent()
    {
        canAttack = true;
    }
    #endregion

    private void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, Vector3.down * model.groundCheckRaycastLenght);
            Gizmos.DrawWireSphere(transform.position, model.wallCheckRaycastLenght);

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 2000))
            {
                var direction = hit.point - transform.position;

                Gizmos.DrawRay(transform.position, direction * 300);
            }
        }
    }
}
