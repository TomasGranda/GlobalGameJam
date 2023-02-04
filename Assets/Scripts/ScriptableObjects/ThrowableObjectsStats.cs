using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ThrowableObjectsStats", menuName = "Stats/ThrowableObjectsStats", order = 0)]
public class ThrowableObjectsStats : ScriptableObject
{
    [Tooltip("Sonido maximo que puede peoducir el Objeto")]
    public float maximumSound = 5;
    [Tooltip("Area en la que se expande el sonido")]
    public float soundArea = 5;

    [Tooltip("Area de Efectos")]
    public float limits;

    [Tooltip("Los objetos que peuden detectar estos sonidos")]
    public LayerMask layerMask;
}