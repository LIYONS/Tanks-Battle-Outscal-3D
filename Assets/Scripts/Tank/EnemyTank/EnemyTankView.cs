using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankView : MonoBehaviour
{
    EnemyTankController controller;
    TankScriptableObject tankObject;

    float movementInput;
    float turnInput;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller.SetTankView(this);
        SetColour();
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
        switch (tankObject.tankType)
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

    public void SetComponents( EnemyTankController _controller, TankScriptableObject _tank)
    {
        controller = _controller;
        tankObject = _tank;
    }
    public Rigidbody GetRigidBody()
    {
        return rb;
    }
}
