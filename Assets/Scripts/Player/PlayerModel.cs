using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerModel
{
    public float speed = 1;

    public float jumpSpeed = 8;

    public float gravityMagnitude = 20;

    public float groundCheckRaycastLenght = 5;
    public float wallCheckRaycastLenght = 5;

    public LayerMask floorMask;

    public LayerMask climbableWallLayer;

    public LayerMask cliffLayer;
}
