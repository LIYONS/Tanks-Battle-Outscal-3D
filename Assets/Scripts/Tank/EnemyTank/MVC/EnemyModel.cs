using UnityEngine;

public class EnemyModel
{
    private TankScriptableObject enemyObject;
    private EnemyController controller;
    public EnemyModel(TankScriptableObject tankScriptableObject)
    {
        enemyObject = tankScriptableObject;
    }

    public TankScriptableObject GetTankObject()
    {
        return enemyObject;
    }
    public void SetTankController(EnemyController _controller)
    {
        controller = _controller;
    }
}
