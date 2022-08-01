using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankSpawner : MonoBehaviour
{
    public EnemyTankView enemyTankView;
    public TankList enemyObjects;

    public Transform[] spawnPoints;
    private void Start()
    {
        SpawnTank();
    }

    void SpawnTank()
    {

        for(int i=0;i<spawnPoints.Length;i++)
        {
            Transform tankTransform= CreateTank();
            tankTransform.position = spawnPoints[i].position;
        }
    }

    Transform CreateTank()
    {
        int index = Random.Range(0, enemyObjects.tankList.Length);
        EnemyTankModel model = new EnemyTankModel(enemyObjects.tankList[index]);
        EnemyTankController controller = new EnemyTankController(model);
        enemyTankView = Instantiate(enemyTankView);
        enemyTankView.SetComponents(controller, enemyObjects.tankList[index]);
        controller.SetTankView(enemyTankView);
        return enemyTankView.transform;
    }

}
