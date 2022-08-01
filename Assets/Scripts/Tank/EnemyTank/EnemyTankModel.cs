using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankModel : MonoBehaviour
{
    TankScriptableObject tankObject;

    EnemyTankController controller;
    public EnemyTankModel(TankScriptableObject tankScriptableObject)
    {
        this.tankObject = tankScriptableObject;
    }

    public float GetMovementSpeed() { return tankObject.movementSpeed; }

    public float GetTurnSpeed() { return tankObject.turnSpeed; }

    public void SetTankController(EnemyTankController _controller)
    {
        controller = _controller;
    }
}
