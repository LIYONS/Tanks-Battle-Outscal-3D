using UnityEngine;

public class TankController
{
    private TankModel tankModel;

    private TankView tankView;

    private Rigidbody rb;

    public TankController(TankView view,TankModel model)
    {
        tankModel = model;
        tankView = GameObject.Instantiate<TankView>(view);
        tankView.SetTankController(this);
        tankModel.SetTankController(this);
        rb = tankView.GetRb();
    }

    public void Move(float movementInput)
    {
        Vector3 movement = rb.gameObject.transform.forward * movementInput * tankModel.GetMoveSpeed() * Time.deltaTime;

        rb.MovePosition(rb.position + movement);
    }

    public void Turn(float turnInput)
    {
        float turn = turnInput * tankModel.GetTurnSpeed() * Time.deltaTime;

        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        rb.MoveRotation(rb.rotation * turnRotation);
    }
}
