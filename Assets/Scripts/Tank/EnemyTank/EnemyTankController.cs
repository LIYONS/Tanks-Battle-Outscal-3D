using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTankController
{
    public EnemyTankModel model;

    public EnemyTankView view;

    //Patrol
    Transform[] wayPoints;
    NavMeshAgent agent;
    int wayPointIndex=0;
    Transform target;


    public EnemyTankController(EnemyTankModel _model)
    {
        this.model = _model;
        wayPoints = model.GetWayPoints();
    }

    public EnemyTankModel GetTankModel()
    {
        return model;
    }
    public EnemyTankView GetTankView()
    {
        return view;
    }

    public void SetTankView(EnemyTankView _tankView)
    {
        view = _tankView;
    }

    void SetAgent()
    {
        agent = view.GetAgent();
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
            temp = Random.Range(0, wayPoints.Length);
        }
        while (temp == wayPointIndex);
        wayPointIndex = temp;
    }
    public Transform GetCurrentTarget()
    {
        return target;
    }
}
