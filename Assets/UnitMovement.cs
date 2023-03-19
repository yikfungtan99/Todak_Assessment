using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private MovementJoystick joystick;

    [Header("Properties")]
    [SerializeField] private float speed = 5f;

    private Vector3 lastDirection = Vector3.zero;

    private void Update()
    {
        // Get the direction of the joystick
        Vector3 joystickDirection = joystick.Direction;
        
        // Move the player in the joystick direction
        transform.rotation = Quaternion.Euler(joystickDirection);

        if (joystickDirection.magnitude > 0f)
        {
            transform.position += transform.forward * speed * Time.deltaTime;

            if (lastDirection != joystick.Direction)
            {
                lastDirection = joystickDirection;
            }
        }
        else
        {
            transform.rotation = Quaternion.Euler(lastDirection);
        }
    }
}
