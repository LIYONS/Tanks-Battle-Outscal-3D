using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankView : MonoBehaviour
{
    public TankController tankController;
    Joystick joyStick;
    TankScriptableObject tankObject;

    float movementInput;
    float turnInput;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        tankController.SetTankView(this);
        SetColour();
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
    void SetSize()
    {
        transform.localScale = new Vector3(tankObject.size, tankObject.size, transform.localScale.z);
    }
    void SetColour()
    {
        Transform tankTurret = gameObject.transform.Find("TankRenderers/TankTurret");
        Transform tankChassis = gameObject.transform.Find("TankRenderers/TankChassis");
        Color color = Color.black;
        switch(tankObject.tankType)
        {
            case TankType.Red:
            {
                    color = Color.red;
                    break;
            }
            case TankType.Blue:
            {
                    color = Color.blue;
                    break;
            }
            case TankType.Green:
            {
                    color = Color.green;
                    break;

             }
        }
        tankTurret.gameObject.GetComponent<MeshRenderer>().materials[0].SetColor("_Color", color);
        tankChassis.gameObject.GetComponent<MeshRenderer>().materials[0].SetColor("_Color", color);

    }
    public void SetComponents(Joystick _joyStick,TankController _controller,TankScriptableObject _tank)
    {
        joyStick = _joyStick;
        tankController = _controller;
        tankObject = _tank;
    }

    public Rigidbody GetRigidBody()
    {
        return rb;
    }
}
