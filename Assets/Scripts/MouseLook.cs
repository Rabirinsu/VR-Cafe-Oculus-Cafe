using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class MouseLook : MonoBehaviour
{
    private float _baseSensitivity = 100f;
    public float mouseSensitivity = 1f;
    public Transform playerBody;
    private float _xRotation;
    
    void Update()
    {
        var mouseX = Input.GetAxisRaw("Mouse X") * (_baseSensitivity * mouseSensitivity) * Time.deltaTime;
        var mouseY = Input.GetAxisRaw("Mouse Y") * (_baseSensitivity * mouseSensitivity) * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}