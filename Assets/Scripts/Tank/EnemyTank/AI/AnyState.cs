using UnityEngine;

public class AnyState : TankState
{
    [SerializeField] private float attackDistance;

    private bool isAttacking;

    public override void OnEnterState()
    {
        base.OnEnterState();
        isAttacking = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            GetComponent<TankChaseState>().SetTarget(other.transform);
            tankView.ChangeState(GetComponent<TankChaseState>());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            float distance = Vector3.Distance(transform.position, other.transform.position);
            if(distance<=attackDistance && !isAttacking)
            {
                GetComponent<TankAttackState>().SetTarget(other.transform);
                tankView.ChangeState(GetComponent<TankAttackState>());
                isAttacking = true;
            }

            if(distance>attackDistance && isAttacking)
            {
                tankView.ChangeState(GetComponent<TankChaseState>());
                isAttacking = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            tankView.ChangeState(GetComponent<TankPatrolState>());
        }
    }
}
