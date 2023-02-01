using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectileController : MonoBehaviour
{
    public float speed = 1;

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
