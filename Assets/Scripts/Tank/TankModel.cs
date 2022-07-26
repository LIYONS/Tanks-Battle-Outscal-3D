using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankModel
{
    TankController tankController;
    float movementSpeed;

    float turnSpeed;


    public TankModel(float _movementSpeed, float _turnSpeed)
    {
        movementSpeed = _movementSpeed;
        turnSpeed = _turnSpeed;
    }

    public float GetMovementSpeed()
    {
        return movementSpeed;
    }

    public float GetTurnSpeed()
    {
        return turnSpeed;
    }
    public void SetTankController(TankController _controller)
    {
        tankController = _controller;
    }
}
