using UnityEngine;
using System.Collections.Generic;

public class EnemyService : MonoSingletonGeneric<EnemyService>
{
    [SerializeField] private EnemyView enemyPrefab;
    [SerializeField] private TankList tankSOList;
    [SerializeField] private int numberOfEnemies;
    [SerializeField] private Transform[] patrolPoints;

    private List<EnemyController> enemyControllers=new();
    private void Start()
    {
        for (int i = 0,spawnPoint=0; i < numberOfEnemies; i++,spawnPoint++)
        {
            SpawnTank(spawnPoint);
            if (spawnPoint == patrolPoints.Length)
            {
                spawnPoint = 0;
            }
        }
    }
    private void SpawnTank(int spawnPoint)
    {
        int index = Random.Range(0, tankSOList.tankSOList.Count);
        EnemyController controller = new(new EnemyModel(tankSOList.tankSOList[index]));
        enemyControllers.Add(controller);
        EnemyView enemyView = Instantiate(enemyPrefab, patrolPoints[spawnPoint].position, patrolPoints[spawnPoint].rotation);
        enemyView.SetController(controller);
        controller.SetTankView(enemyView);
    }

    public Transform[] GetPatrolPoints()
    {
        return patrolPoints;
    }
}
