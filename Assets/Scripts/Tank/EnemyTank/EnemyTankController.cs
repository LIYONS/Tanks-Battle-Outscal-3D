using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTankController
{
    public EnemyTankModel tankModel;

    public EnemyTankView tankView;

    //Patrol
    private Transform[] wayPoints;
    private NavMeshAgent agent;
    private int wayPointIndex =0;
    private Transform target;

    private TankScriptableObject tankObject;
    private float currentHealth;
    private bool isDead;
    public EnemyTankController(EnemyTankModel _model)
    {
        this.tankModel = _model;
        tankObject = tankModel.GetTankObject();
        wayPoints = tankModel.GetWayPoints();
        currentHealth = tankObject.maxHealth;
        isDead = false;
    }
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        tankView.SetHealthUI(currentHealth);
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            OnDeath();
        }
    }
    private void OnDeath()
    {
        tankView.OnDeath();
        tankView.gameObject.SetActive(false);
    }

    public EnemyTankModel GetTankModel()
    {
        return tankModel;
    }
    public EnemyTankView GetTankView()
    {
        return tankView;
    }

    public void SetTankView(EnemyTankView _tankView)
    {
        tankView = _tankView;
    }

    void SetAgent()
    {
        agent = tankView.GetAgent();
    }

    public void Patrol()
    {
        IterateWayPointIndex();
        SetAgent();
        target = wayPoints[wayPointIndex];
        agent.SetDestination(target.position);
    }

    void  IterateWayPointIndex()
    {
        int temp=0;
        do
        {
            temp = UnityEngine.Random.Range(0, wayPoints.Length);
        }
        while (temp == wayPointIndex);
        wayPointIndex = temp;
    }
    public Transform GetCurrentTarget()
    {
        return target;
    }
}
