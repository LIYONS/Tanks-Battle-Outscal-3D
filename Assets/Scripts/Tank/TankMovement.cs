using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public float speed;
    public float turnSpeed;
    public Joystick joystick;

    Rigidbody rb;
    float movementInput; 
    float turnInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        movementInput = joystick.Vertical;
        turnInput = joystick.Horizontal;
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
    }
    void Move()
    {
        Vector3 movement = transform.forward * movementInput * speed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }

    void Turn()
    {
        float turn = turnInput*0.3f * turnSpeed * Time.deltaTime;

        Quaternion turnValue = Quaternion.Euler(0f, turn, 0f);

        rb.MoveRotation(rb.rotation * turnValue);
    }
}
