using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : Targetable
{
    public float explotionRadio = 1;

    public bool debug;

    public override void OnDeath()
    {
        base.OnDeath();

        Collider[] colliders = Physics.OverlapSphere(transform.position, explotionRadio);

        foreach (Collider collider in colliders)
        {
            Debug.Log(collider.name + "is death");
        }
    }

    private void OnDrawGizmos()
    {
        if (!debug) return;

        Gizmos.color = Color.red;

        Gizmos.DrawSphere(transform.position, explotionRadio);
    }
}
