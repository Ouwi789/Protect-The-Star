using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControls : MonoBehaviour
{
    //TODO change movement so it rotates around the sun as the WASD movement feels clunky

    private bool _isRotating;

    [SerializeField] private float rotationSpeed = 1000f;

    public void OnRotate(InputAction.CallbackContext ctc)
    {
        _isRotating = ctc.started || ctc.performed;
    }

    private void LateUpdate()
    {
        if(_isRotating)
        {
            float verticalInput = -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
            float horizontalInput = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.right, verticalInput);
            transform.Rotate(Vector3.up, horizontalInput, Space.World);
        }
    }
}
