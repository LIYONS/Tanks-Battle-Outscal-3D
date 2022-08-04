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
        if(Vector3.Distance(transform.position,controller.GetCurrentTarget().position)<2f)
        {
            controller.Patrol();
        }
    }
    void SetSize()
    {
        transform.localScale = new Vector3(tankObject.size, tankObject.size, tankObject.size);
    }
    void SetColour()
    {
        Transform tankTurret = gameObject.transform.Find("TankRenderers/TankTurret");
        Transform tankChassis = gameObject.transform.Find("TankRenderers/TankChassis");
        tankTurret.gameObject.GetComponent<MeshRenderer>().materials[0].SetColor("_Color", tankObject.tankTurretColor);
        tankChassis.gameObject.GetComponent<MeshRenderer>().materials[0].SetColor("_Color", tankObject.tankChassisColor);
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
