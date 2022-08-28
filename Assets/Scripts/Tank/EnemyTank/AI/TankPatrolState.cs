using UnityEngine;
using UnityEngine.AI;


namespace TankGame.Tanks.EnemyServices
{
    public class TankPatrolState : TankState
    {
        private NavMeshAgent agent;
        private Transform[] wayPoints;
        private int wayPointIndex;
        private Transform target;
        public override void OnEnterState()
        {
            base.OnEnterState();
            agent = GetComponent<NavMeshAgent>();
            wayPoints = EnemyService.Instance.GetPatrolPoints();
            Patrol();
        }
        public override void OnExitState()
        {
            base.OnExitState();
        }
        private void FixedUpdate()
        {
            if (agent.remainingDistance < 2f)
            {
                tankView.ChangeState(GetComponent<TankIdleState>());
            }
            else if (agent.remainingDistance < 5f && agent.isStopped == true)
            {
                agent.ResetPath();
                agent.SetDestination(target.position);
            }
        }

        public void Patrol()
        {
            IterateWayPointIndex();
            target = wayPoints[wayPointIndex];
            agent.SetDestination(target.position);
        }

        void IterateWayPointIndex()
        {
            int temp;
            do
            {
                temp = Random.Range(0, wayPoints.Length);
            }
            while (temp == wayPointIndex);
            wayPointIndex = temp;
        }
    }
}
