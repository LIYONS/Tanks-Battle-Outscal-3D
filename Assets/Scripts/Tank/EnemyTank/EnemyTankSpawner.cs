using UnityEngine;

public class EnemyTankSpawner : MonoBehaviour
{
    public EnemyTankView view;
    public TankList enemyObjects;
    public int enemyCount;
    public Transform[] wayPoints;


    private EnemyTankController controller;
    private int scriptableObjectIndex;
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

            view = Instantiate(view,wayPoints[point].position,wayPoints[point].rotation);

            view.SetComponents(controller, enemyObjects.tankList[scriptableObjectIndex],wayPoints);
            controller.SetTankView(view);
        }
    }
}
