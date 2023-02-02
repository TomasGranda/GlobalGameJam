using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CollisionSound : MonoBehaviour
{
    public ThrowableObjectsStats throwableObjects;

    private void OnCollisionEnter(Collision other)
    {
        Collider[] players = Physics.OverlapSphere(transform.position, throwableObjects.soundArea, throwableObjects.layerMask);

        foreach (var player in players)
        {
            var soundProvoked = throwableObjects.maximumSound * ((player.transform.position - transform.position).magnitude / throwableObjects.soundArea);

            if (player.GetComponent<Entity>())
            {
                player.GetComponent<Entity>().DetectCollisionSound(soundProvoked);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, throwableObjects.soundArea);
    }
}