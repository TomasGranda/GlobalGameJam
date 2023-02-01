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

    private Commands commands;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Commands
        commands = new Commands();

        MoveCommand moveCommand = new MoveCommand(this);
        JumpCommand jumpCommand = new JumpCommand(this);
        ClimbCommand climbCommand = new ClimbCommand(this);
        ClimbCliffCommand climbCliffCommand = new ClimbCliffCommand(this);
        LeaveCliffCommand leaveCliffCommand = new LeaveCliffCommand(this);

        commands.AddCommand(climbCommand);
        commands.AddCommand(climbCliffCommand);
        commands.AddCommand(leaveCliffCommand);
        commands.AddCommand(moveCommand);
        commands.AddCommand(jumpCommand);
    }

    void Update()
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

    #region RaycastCheckers
    public void CheckWalls()
    {
        // TODO: Replace all Vector3.right to transform.forward
        var newIsClimbing = Physics.Raycast(transform.position, Vector3.right, model.wallCheckRaycastLenght, model.climbableWallLayer);

        isOnCliff = Physics.Raycast(transform.position, Vector3.right, model.wallCheckRaycastLenght, model.cliffLayer);

        if (isClimbing && isClimbing != newIsClimbing && !isOnCliff)
        {
            characterController.Move(Vector3.left * .1f);
        }
        isClimbing = newIsClimbing;
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

    public bool GetClimbCliffButtonDown()
    {
        return inputAsset.FindAction("ClimbCliff").WasPressedThisFrame();
    }

    public bool GetLeaveCliffButtonDown()
    {
        return inputAsset.FindAction("LeaveCliff").WasPressedThisFrame();
    }
    #endregion

    public bool IsOnClimb()
    {
        return isClimbing || isOnCliff;
    }

    private void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, Vector3.down * model.groundCheckRaycastLenght);
            Gizmos.DrawWireSphere(transform.position, model.wallCheckRaycastLenght);
        }
    }
}
