using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{

    [SerializeField] float movementSpeed;
    [SerializeField] float turnSpeed;
    [SerializeField] Joystick joyStick;

    Rigidbody rb;
    float movementInput;
    float turnInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        movementInput = 0f;
        turnInput = 0f;
    }
    private void Update()
    {
        movementInput = joyStick.Vertical;
        turnInput = joyStick.Horizontal;    
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    private void Move()
    {
        Vector3 movement = transform.forward * movementInput * movementSpeed * Time.deltaTime;

        rb.MovePosition(rb.position + movement);
    }

    private void Turn()
    {
        float turn = turnInput * turnSpeed * Time.deltaTime;

        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        rb.MoveRotation(rb.rotation * turnRotation);
    }
}

