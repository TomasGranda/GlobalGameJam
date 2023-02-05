using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectileController : MonoBehaviour
{
    public float force = 1;

    void Update()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * force);
    }
}
