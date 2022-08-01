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
    EnemyTankModel model;
    int scriptableObjectIndex;
    private void Start()
    {
        SpawnTank();
    }
    private void SpawnTank()
    {
        for(int i=0;i<enemyCount;i++)
        {
            int point = Random.Range(0, wayPoints.Length);


            scriptableObjectIndex = Random.Range(0, enemyObjects.tankList.Length);
            EnemyTankModel model = new EnemyTankModel(enemyObjects.tankList[scriptableObjectIndex], wayPoints);

            controller = new EnemyTankController(model);

            view = Instantiate(view,wayPoints[point]);

            view.SetComponents(controller, enemyObjects.tankList[scriptableObjectIndex]);
            controller.SetTankView(view);
        }
    }
}
