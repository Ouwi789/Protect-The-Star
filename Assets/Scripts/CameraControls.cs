using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControls : MonoBehaviour
{
    private Vector2 _delta;

    private bool _isRotating;
    private float X;
    private float Y;

    [SerializeField] private float rotationSpeed = 3f;

    private void Awake()
    {
        X = transform.rotation.eulerAngles.x;
        Y = transform.rotation.eulerAngles.y;
    }

    public void OnRotate(InputAction.CallbackContext ctc)
    {
        _isRotating = ctc.started || ctc.performed;
    }

    private void LateUpdate()
    {
        if(_isRotating)
        {
            transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * rotationSpeed, -Input.GetAxis("Mouse X") * rotationSpeed, 0));
            X = transform.rotation.eulerAngles.x;
            Y = transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(X, Y, 0);
        }
    }
}
