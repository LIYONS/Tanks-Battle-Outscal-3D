using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTankController
{
    PlayerTankModel tankModel;

    PlayerTankView tankView;

    Rigidbody rb;

    float movementSpeed;
    float turnSpeed;

    public PlayerTankController(PlayerTankModel _model)
    {
        tankModel = _model;
        tankModel.SetTankController(this);
        GetData();
    }
    public void Movement(float movementInput)
    {
        Vector3 movement = tankView.gameObject.transform.forward * movementInput * movementSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }

    public void Rotate(float turnInput)
    {
        
        float turn = turnInput * 0.5f * turnSpeed * Time.deltaTime;

        Quaternion turnValue = Quaternion.Euler(0f, turn, 0f);

        rb.MoveRotation(rb.rotation * turnValue);
    }

    public PlayerTankModel GetTankModel()
    {
        return tankModel;
    }
    public PlayerTankView GetTankView()
    {
        return tankView;
    }

    public void SetTankView(PlayerTankView _tankView)
    {
        tankView = _tankView;
        rb = tankView.GetRigidBody();
    }
    void GetData()
    {
        movementSpeed = tankModel.GetMovementSpeed();
        turnSpeed = tankModel.GetTurnSpeed();
    }
}
