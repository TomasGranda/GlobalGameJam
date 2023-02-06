using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : Targetable
{
    public float explotionRadio = 1;

    public bool debug;

    public GameObject boom;

    public override void OnDeath()
    {
        base.OnDeath();

        boom.SetActive(true);
        boom.transform.parent = null;

        Collider[] colliders = Physics.OverlapSphere(transform.position, explotionRadio);

        foreach (Collider collider in colliders)
        {
            collider.GetComponent<IDamage>()?.Damage(2000);
        }

        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        if (!debug) return;

        Gizmos.color = Color.red;

        Gizmos.DrawSphere(transform.position, explotionRadio);
    }
}
