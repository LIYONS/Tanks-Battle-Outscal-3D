using UnityEngine;

public class AnyState : TankState
{
    [SerializeField] private float chaseRadius;
    [SerializeField] private float attackDistance;
    [SerializeField] private LayerMask tankLayer;

    private bool isAttacking;
    private bool isChasing;
    private float distance;
    public void Start()
    {
        isAttacking = false;
        isChasing = false;
    }

    private void Update()
    {
        Transform target = TargetInRange();
        if(target!=null)
        {
            if(!isChasing && !isAttacking)
            {
                Debug.Log("Chase");
                GetComponent<TankChaseState>().SetTarget(target);
                tankView.ChangeState(GetComponent<TankChaseState>());
                isChasing = true;
            }
            distance = Vector3.Distance(transform.position, target.position);
            if (distance < attackDistance && !isAttacking)
            {
                Debug.Log("Attack");
                GetComponent<TankAttackState>().SetTarget(target);
                tankView.ChangeState(GetComponent<TankAttackState>());
                isAttacking = true;
                isChasing = false;
            }
        }
        if (distance > attackDistance && isAttacking)
        {
            Debug.Log("leave Attack");
            tankView.ChangeState(GetComponent<TankChaseState>());
            isAttacking = false;
            isChasing = true;
        }
        if(target==null && isChasing)
        {
            Debug.Log("leave chase");
            tankView.ChangeState(GetComponent<TankPatrolState>());
            isChasing = false;
        }
        
    }
    private Transform TargetInRange()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, chaseRadius, tankLayer);
        for(int i=0;i<colliders.Length;i++)
        {
            if(colliders[i].tag=="Player")
            {
                return colliders[i].transform;
            }
        }
        return null;
    }
}
