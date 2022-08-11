using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankIdleState : TankState
{
     private float idleTime;
     private float timeElapsed;
    public override void OnEnterState()
    {
        base.OnEnterState();
        idleTime = Random.Range(0, 10);
    }

    public override void OnExitState()
    {
        base.OnExitState();
        timeElapsed = 0;
    }
    private void Update()
    {
        Tick();
    }
    public override void Tick()
    {
        timeElapsed += Time.deltaTime;
        if(timeElapsed>idleTime)
        {
            tankView.ChangeState(GetComponent<TankPatrolState>());
        }
    }
}
