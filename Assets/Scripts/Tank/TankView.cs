using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankView : MonoBehaviour
{
    private TankController tankController;
    float movementInput;
    float turnInput;
    public void SetTankController(TankController controller)
    {
        tankController = controller;
    }

    private void Update()
    {
        movementInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }
    private void FixedUpdate()
    {
        tankController.Move(movementInput);
        tankController.Turn(turnInput);
    }
    public Rigidbody GetRb()
    {
        return GetComponent<Rigidbody>();
    }
}
