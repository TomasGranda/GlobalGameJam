using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steerings
{
    public static Vector3 Seek(Transform Origin, Transform Target)
    {
        Vector3 desired_direction = new Vector3();
        Vector3 moveVector;

        desired_direction = Target.position - Origin.position;

        moveVector = desired_direction;

        return moveVector.normalized;
    }

    public static Vector3 Evade(Transform Origin, float range, Vector3 Target)
    {
        Vector3 desired_direction = new Vector3();
        Vector3 moveVector;

        desired_direction = Origin.position - Target;

        if (desired_direction.magnitude < range)
        {

            moveVector = desired_direction.normalized;

            Debug.DrawRay(Origin.position, moveVector, Color.magenta);
            return moveVector;

        }
        else
        {

            return Vector3.zero;
        }
    }

    public static Vector3 Separate(Transform Origin, List<Transform> Targets)
    {
        Vector3 desired_direction = new Vector3();

        Vector3 moveVector;

        float radius = 4f;

        moveVector = desired_direction;

        foreach (var neighbor in Targets)
        {
            if (neighbor != null)
            {
                if (Vector3.Distance(Origin.position, neighbor.position) < radius)
                {

                    desired_direction = neighbor.position - Origin.position;

                    float scale = desired_direction.magnitude / (float)Mathf.Sqrt(radius);

                    Debug.DrawRay(Origin.position, desired_direction / scale, Color.blue);

                    moveVector += desired_direction / scale;
                }
            }
        }

        moveVector /= Targets.Count;

        moveVector *= -1;
        return moveVector;
    }

    public static Vector3 Follow(Transform Origin, Transform Target)
    {
        Vector3 desired_direction = new Vector3();
        Vector3 moveVector;

        desired_direction = (Target.TransformPoint(new Vector3(0.0f, 0.0f, -1.15f))) - Origin.position;

        Debug.DrawLine(Origin.position, Origin.position + desired_direction, Color.red);

        moveVector = (desired_direction);

        return moveVector;
    }

    public static Vector3 Wander(Transform Origin)
    {
        float maxJitter = 0.10f;
        Vector3 desired_direction = Origin.forward;
        desired_direction = new Vector3(desired_direction.x + Random.Range(-maxJitter, maxJitter), 0, desired_direction.z + Random.Range(-maxJitter, maxJitter));
        return desired_direction;
    }

    public static Vector3 Avoid(Transform Origin)
    {
        Rigidbody rb = Origin.gameObject.GetComponent<Rigidbody>();

        int max_velocity = 1;

        Vector3 ahead = Origin.position + Origin.forward + (Mathf.Min(rb.velocity.magnitude, 10) * rb.velocity.normalized);

        List<Vector3> most_threatening = new List<Vector3>();
        Collider[] hit_Colliders = Physics.OverlapSphere(ahead, 2f);

        int closest_distance_index = 0;
        Vector3 direction_to_obstacle = new Vector3();

        foreach (var item in hit_Colliders)
        {
            if (item.CompareTag("Obstacle") || item.CompareTag("Enemy") && Origin.gameObject.CompareTag("Leader"))
            {
                most_threatening.Add(item.ClosestPoint(Origin.position));
            }
        }

        for (int i = 0; i < most_threatening.Count; i++)
        {
            if (Vector3.Dot((most_threatening[i] - Origin.position).normalized, Origin.forward) > 0.5f)
            {
                if (Vector3.Distance(Origin.position, most_threatening[i]) >
                    Vector3.Distance(Origin.position, most_threatening[closest_distance_index]))
                {
                    closest_distance_index = i;
                    Debug.DrawLine(Origin.position, most_threatening[i], Color.green);
                }
            }
        }

        if (most_threatening.Count != 0)
        {
            Debug.DrawLine(Origin.position, most_threatening[closest_distance_index], Color.red);
            Debug.DrawLine(Origin.position, ahead, Color.cyan);

            direction_to_obstacle = most_threatening[closest_distance_index] - Origin.position;

            direction_to_obstacle.y = 0.0f;

            Vector3 avoid_direction = new Vector3(-direction_to_obstacle.z, direction_to_obstacle.y, direction_to_obstacle.x).normalized;

            float sign = Mathf.Sign(Vector3.Dot(Origin.right, direction_to_obstacle));

            float forceMult = Mathf.Min(direction_to_obstacle.magnitude * 10, max_velocity);

            Debug.DrawRay(Origin.position, avoid_direction * forceMult * sign, Color.blue);

            return (avoid_direction * forceMult * sign);

        }
        else
        {
            return Vector3.zero;
        }
    }
}