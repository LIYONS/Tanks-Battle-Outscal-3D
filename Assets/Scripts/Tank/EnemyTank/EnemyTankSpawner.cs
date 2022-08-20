using UnityEngine;

public class EnemyTankSpawner : MonoSingletonGeneric<EnemyTankSpawner>
{
    [SerializeField] private EnemyTankView view;
    [SerializeField] private TankList tankSOList;
    [SerializeField] private int numberOfEnemies;
    [SerializeField] private Transform[] patrolPoints;


    private EnemyTankController controller;
    private int scriptableObjectIndex;
    private void Start()
    {
        SpawnTank();
    }
    private void SpawnTank()
    {
        for(int i=0,point=0;i<numberOfEnemies;i++,point++)
        {
            if(point==patrolPoints.Length)
            {
                point = 0;
            }
            scriptableObjectIndex = Random.Range(0, tankSOList.tankSOList.Count);
            EnemyTankModel model = new EnemyTankModel(tankSOList.tankSOList[scriptableObjectIndex], patrolPoints);

            controller = new EnemyTankController(model);

            view = Instantiate(view,patrolPoints[point].position,patrolPoints[point].rotation);

            view.SetComponents(controller, tankSOList.tankSOList[scriptableObjectIndex],patrolPoints);
            controller.SetTankView(view);
        }
    }
}
