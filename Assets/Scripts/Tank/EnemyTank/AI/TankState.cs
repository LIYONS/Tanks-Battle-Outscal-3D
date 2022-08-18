using UnityEngine;

[RequireComponent(typeof(EnemyTankView))]
public abstract class TankState : MonoBehaviour
{
    protected EnemyTankView tankView;

    private void Awake()
    {
        tankView = GetComponent<EnemyTankView>();
    }
    public virtual void OnEnterState()
    {
        this.enabled = true;
    }

    public virtual void OnExitState()
    {
        this.enabled = false;
    }
}
