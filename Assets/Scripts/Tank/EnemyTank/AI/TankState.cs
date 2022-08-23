using UnityEngine;

[RequireComponent(typeof(EnemyView))]
public abstract class TankState : MonoBehaviour
{
    protected EnemyView tankView;

    private void Awake()
    {
        tankView = GetComponent<EnemyView>();
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
