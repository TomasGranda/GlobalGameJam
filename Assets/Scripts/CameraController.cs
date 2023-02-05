using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    void LateUpdate()
    {
        Destroy(targetImage);
        targetImage = null;

        TargetablePoint target = Utils.GetTargetPoint();
        if (target != null)
            targetImage = GameObject.Instantiate(targetImagePrefab, Camera.main.WorldToScreenPoint(target.transform.position), Quaternion.identity, canvas.transform);
    }
}
