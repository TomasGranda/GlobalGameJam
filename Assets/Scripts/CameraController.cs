using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;

    private Vector3 initialPosition;

    public LayerMask targetableLayer;

    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        initialPosition = transform.position;
    }

    void Update()
    {
        var newPosition = new Vector3(initialPosition.x + player.position.x, initialPosition.y, initialPosition.z);
        transform.position = newPosition;

        // TODO: Change to New Input System
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2000))
        {
            
        }
    }
}
