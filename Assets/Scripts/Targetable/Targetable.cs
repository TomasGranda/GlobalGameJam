using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetable : MonoBehaviour
{
    public List<TargetablePoint> targetablePoints = new List<TargetablePoint>();

    private void OnTriggerEnter(Collider other)
    {
        var proyectile = other.GetComponent<ProyectileController>();
        if (proyectile != null)
        {
            var dmg = GetComponent<IDamage>();
            if (dmg != null) dmg.Damage(200);

            Destroy(proyectile.gameObject);
            OnDeath();
        }
    }

    public virtual void OnDeath()
    {

    }
}
