using UnityEngine;

public class TankModel
{
    private TankController tankController;

    private float movementSpeed;
    private float turnSpeed;

    public TankModel(float _movementSpeed,float _turnSpeed)
    {
        movementSpeed =_movementSpeed;
        turnSpeed = _turnSpeed;
    }
    public void SetTankController(TankController controller)
    {
        tankController = controller;
    }
    public float GetMoveSpeed()
    {
        return movementSpeed;
    }
    public float GetTurnSpeed()
    {
        return turnSpeed;
    }
}
