using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTankView : MonoBehaviour
{
    EnemyTankController controller;
    TankScriptableObject tankObject;
    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        controller.SetTankView(this);
        SetSize();
        SetColour();
        controller.Patrol();
    }
    private void FixedUpdate()
    {
        if(Vector3.Distance(transform.position,controller.GetCurrentTarget().position)<1)
        {
            controller.Patrol();
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
    public NavMeshAgent GetAgent()
    {
        return agent;
    }
}
