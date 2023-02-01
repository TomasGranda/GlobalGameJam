using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;

    private Vector3 initialPosition;

    public LayerMask targetableLayer;

    public GameObject targetImagePrefab;

    private GameObject targetImage;

    public Canvas canvas;


    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        initialPosition = transform.position;
    }

    void Update()
    {
        Destroy(targetImage);
        targetImage = null;

        var newPosition = new Vector3(initialPosition.x + player.position.x, initialPosition.y, initialPosition.z);
        transform.position = newPosition;

        // TODO: Change to New Input System
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2000) && hit.collider.transform.GetComponent<Targetable>() != null)
        {
            targetImage = Instantiate(targetImagePrefab, Camera.main.WorldToScreenPoint(hit.collider.transform.position), Quaternion.identity, canvas.transform);
        }
    }
}
