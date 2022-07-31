using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankModel
{
    TankController tankController;

    TankScriptableObject tankObject;

    public TankModel(TankScriptableObject tankScriptableObject)
    {
        this.tankObject = tankScriptableObject;
    }

    public float GetMovementSpeed() { return tankObject.movementSpeed; }

    public float GetTurnSpeed() { return tankObject.turnSpeed;}

    public void SetTankController(TankController _controller)
    {
        tankController = _controller;
    }
}
