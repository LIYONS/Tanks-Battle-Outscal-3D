using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTankModel
{
    PlayerTankController tankController;

    TankScriptableObject tankObject;

    public PlayerTankModel(TankScriptableObject tankScriptableObject)
    {
        this.tankObject = tankScriptableObject;
    }

    public float GetMovementSpeed() { return tankObject.movementSpeed; }

    public float GetTurnSpeed() { return tankObject.turnSpeed;}

    public void SetTankController(PlayerTankController _controller)
    {
        tankController = _controller;
    }
}
