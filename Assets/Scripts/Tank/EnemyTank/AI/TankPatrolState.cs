using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankPatrolState : TankState
{
    public override void OnEnterState()
    {
        base.OnEnterState();
    }

    public override void OnExitState()
    {
        base.OnExitState();
    }
    private void Update()
    {
        Tick();
    }
    public override void Tick()
    {
    }
}
