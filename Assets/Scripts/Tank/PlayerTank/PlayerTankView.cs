using UnityEngine;

public class PlayerTankView : MonoBehaviour
{
    public PlayerTankController tankController;
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
        turnInput = Input.GetAxis("Horizontal");
        if(turnInput!=0)
        {
            tankController.Rotate(turnInput);
        }

    }

    private void Move()
    {
        movementInput = Input.GetAxis("Vertical");
        if(movementInput!=0)
        {
            tankController.Movement(movementInput);
        }
    }
    void SetColour()
    {
        Transform tankTurret = gameObject.transform.Find("TankRenderers/TankTurret");
        Transform tankChassis = gameObject.transform.Find("TankRenderers/TankChassis");
        tankTurret.gameObject.GetComponent<MeshRenderer>().materials[0].SetColor("_Color", tankObject.tankTurretColor);
        tankChassis.gameObject.GetComponent<MeshRenderer>().materials[0].SetColor("_Color", tankObject.tankChassisColor);

    }
    public void SetComponents(PlayerTankController _controller,TankScriptableObject _tank)
    {
        tankController = _controller;
        tankObject = _tank;
    }

    public Rigidbody GetRigidBody()
    {
        return rb;
    }
}
