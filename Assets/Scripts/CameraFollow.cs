using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Update()
    {
        var vector3 = transform.position;
        vector3.y = target.position.y;
        transform.position = vector3;
    }
}
