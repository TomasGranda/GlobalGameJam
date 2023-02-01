using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetable : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        var proyectile = other.GetComponent<ProyectileController>();
        if (proyectile != null)
        {
            Debug.Log("Death");
            Destroy(proyectile.gameObject);
        }
    }
}
