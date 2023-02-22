using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [HideInInspector] public bool isRotating;
    [HideInInspector] public bool clockwise;
    private float _rotationSpeed = 100f;


    private void Update()
    {
        if (isRotating)
        {
            if (clockwise)
            {
                transform.Rotate(new Vector3(0, -1, 0) * _rotationSpeed * Time.deltaTime, Space.World);
                if (transform.localEulerAngles.y - 224 >= 0 && transform.localEulerAngles.y - 224 <= 1)
                {
                    isRotating = false;
                }
            }
            else
            {
                transform.Rotate(new Vector3(0, 1, 0) * _rotationSpeed * Time.deltaTime, Space.World);
                if (transform.localEulerAngles.y - 35 >= 0 && transform.localEulerAngles.y - 35 <= 1)
                {
                    isRotating = false;
                }
            }
        }
    }
}
