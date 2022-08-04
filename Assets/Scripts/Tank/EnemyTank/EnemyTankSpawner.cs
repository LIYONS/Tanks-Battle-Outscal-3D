using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankSpawner : MonoBehaviour
{
    public EnemyTankView view;
    public TankList enemyObjects;
    public int enemyCount;
    public Transform[] wayPoints;
    EnemyTankController controller;
    int scriptableObjectIndex;
    private void Start()
    {
        SpawnTank();
    }
    private void SpawnTank()
    {
        for(int i=0,point=0;i<enemyCount;i++,point++)
        {
            if(point==wayPoints.Length)
            {
                point = 0;
            }
            scriptableObjectIndex = Random.Range(0, enemyObjects.tankList.Length);
            EnemyTankModel model = new EnemyTankModel(enemyObjects.tankList[scriptableObjectIndex], wayPoints);

            controller = new EnemyTankController(model);

            view = GameObject.Instantiate(view);
            view.transform.parent =null;
            view.transform.position = wayPoints[point].position;

            view.SetComponents(controller, enemyObjects.tankList[scriptableObjectIndex]);
            controller.SetTankView(view);
        }
    }
}
