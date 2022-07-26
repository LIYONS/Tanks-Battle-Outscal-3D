using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankView : MonoBehaviour
{
    public TankController tankController;
    Joystick joyStick;

    float movementInput;
    float turnInput;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        tankController.SetTankView(this);
    }
    void FixedUpdate()
    {
        Move();
        Turn();
    }

    private void Turn()
    {
        turnInput = joyStick.Horizontal;
        if(turnInput!=0)
        {
            tankController.Rotate(turnInput);
        }

    }

    private void Move()
    {
        movementInput = joyStick.Vertical;
        if(movementInput!=0)
        {
            tankController.Movement(movementInput);
        }
    }


    public void SetJoyStick(Joystick _joyStick)
    {
        joyStick = _joyStick;
    }

    public Rigidbody GetRigidBody()
    {
        return rb;
    }

    public void SetTankController(TankController _controller)
    {
        tankController = _controller;
    }
}
