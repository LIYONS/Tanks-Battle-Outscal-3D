using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankModel
{
    TankScriptableObject tankObject;

    Transform[] wayPoints;

    EnemyTankController controller;
    public EnemyTankModel(TankScriptableObject tankScriptableObject,Transform[] _targetPoints)
    {
        this.tankObject = tankScriptableObject;
        this.wayPoints = _targetPoints;
    }

    public float GetMovementSpeed() { return tankObject.movementSpeed; }

    public float GetTurnSpeed() { return tankObject.turnSpeed; }

    public void SetTankController(EnemyTankController _controller)
    {
        controller = _controller;
    }

    public Transform[] GetWayPoints()
    {
        return wayPoints;
    }
}
