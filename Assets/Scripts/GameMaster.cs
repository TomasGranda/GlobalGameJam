using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster Instance;

    public LayerMask noInvisibleWallLayer;

    private void Awake()
    {
        Instance = this;
    }
}
