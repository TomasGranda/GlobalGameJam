using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerModel
{
    [Header("Stats")]
    public float speed = 1;

    public float jumpSpeed = 8;

    public float gravityMagnitude = 20;

    [Header("Detection")]
    public float groundCheckRaycastLenght = 5;
    
    public float wallCheckRaycastLenght = 5;

    public LayerMask floorMask;

    public LayerMask climbableWallLayer;

    public LayerMask cliffLayer;

    [Space]
    public ProyectileController proyectilePrefab;


    [Header("Vertical Movement Settings")]
    public int verticalMovementStepSize;

    public int maxVerticalSteps = 0;
}
