using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityStats", menuName = "Stats/EntityStats", order = 0)]
public class EntityStats : ScriptableObject
{
    [Header("Vida Maxima")]
    public float maxLife;

    [Header("Velociadad de moviento")]
    public float minMoveSpeed = 1;
    public float maxMoveSpeed = 4;

    public float minRotateSpeed = 1;
    public float maxRotateSpeed = 5;

    [Header("Rango de vision")]
    public float rangeVision;
    public float angleVision;

    [Header("Rango de Ataque")]
    public float rangeAttack;

    [Header("Limite de Deteccion de Sonido")]
    public float limitSound;
}