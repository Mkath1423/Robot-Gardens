using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraMover : MonoBehaviour
{
    public float speedLow = 10f;
    public float speedHigh = 30;
    public float dragSpeed = 2f;

    public float MaxSize;
    public float MinSize;
    public float zoomSpeed;

    public float friction = 10f;
    public float acceleration = 10f;

    private Vector3 currentVelocity;
    private Vector3 direction;

    public StringReference mouseState;

    private bool drag = false;
    private Vector3 Origin;
    private Vector3 Difference;

    void LateUpdate()
    {
        if (mouseState.Value == "Camera")
        {
            if (Input.GetMouseButton(0))
            {
                Difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;

                if (!drag)
                {
                    drag = true;
                    Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                }
            }
            else
            {
                drag = false;
            }
        }
        else
        {
            drag = false;
        }

        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize + (Input.mouseScrollDelta.y * zoomSpeed), MinSize, MaxSize);

        if (drag)
        {
            Camera.main.transform.position = Origin - Difference;
        }
        else
        {
            drag = false;

            float speed = Input.GetButton("SpeedUp") ? speedHigh : speedLow;

            direction = Vector3.Normalize(new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));

            // Lerp to find new velocity
            if (direction.x == 0f && direction.y == 0f)
            {
                currentVelocity = Vector3.Lerp(currentVelocity, Vector3.zero, friction * Time.deltaTime);
            }
            else
            {
                currentVelocity = Vector3.Lerp(currentVelocity, direction * speed, acceleration * Time.deltaTime);
            }


            // Apply Velocity
            //Debug.Log($"Direction: {direction}");
            //Debug.Log($"Velocity:  {currentVelocity}");s
            transform.position += new Vector3(currentVelocity.x * Time.deltaTime * Mathf.InverseLerp(MinSize - 3, MaxSize, Camera.main.orthographicSize),
                                               currentVelocity.y * Time.deltaTime * Mathf.InverseLerp(MinSize - 3, MaxSize, Camera.main.orthographicSize));
        }
       
       
    }
}
