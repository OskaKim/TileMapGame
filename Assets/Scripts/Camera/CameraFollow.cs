using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0.0f, 0.0f, -15.0f);
    [SerializeField] private float smoothSpeed = 7.0f;

    private void LateUpdate()
    {
        var desiredPosition = target.localPosition + offset;
        var smoothPosition = Vector3.Lerp(transform.localPosition, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.localPosition = smoothPosition;
    }
}
