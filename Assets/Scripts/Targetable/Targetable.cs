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
            Debug.Log("Death");
            Destroy(proyectile.gameObject);
        }
    }

    public virtual void OnDeath()
    {

    }
}
