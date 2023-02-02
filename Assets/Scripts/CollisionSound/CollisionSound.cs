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
            // var soundProvoked = throwableObjects.maximumSound * ((player.transform.position - transform.position).magnitude / throwableObjects.soundArea);

            var collisionTarget = player.GetComponent<IDetectionSound>();

            if (collisionTarget != null)
            {
                collisionTarget.DetectCollisionSound(transform.position);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, throwableObjects.soundArea);
    }
}