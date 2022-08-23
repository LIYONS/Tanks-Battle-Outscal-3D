using UnityEngine;
using System.Collections.Generic;

public class EnemyService : MonoSingletonGeneric<EnemyService>
{
    [SerializeField] private EnemyView enemyPrefab;
    [SerializeField] private TankList tankSOList;
    [SerializeField] private int numberOfEnemies;
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float timeBtwSpawns;
    [SerializeField] private float spawnDelay;

    private float timer;
    private int currentEnemyCount=0;
    private int spawnPointIndex;
    private List<EnemyController> enemyControllers=new();
    private void Start()
    {
        timer = timeBtwSpawns;
        spawnPointIndex = 0;
        for (int i = 0; i < numberOfEnemies; i++)
        {
            SpawnTank();
        }
    }
    private void OnEnable()
    {
        EventHandler.Instance.OnEnemyDeath += OnEnemyDead;
    }
    private void Update()
    {
        if(timer<Time.time)
        {
            SpawnTank();
            timer = Time.time + timeBtwSpawns;
        }
    }
    private void SpawnTank()
    {
        if (spawnPointIndex == patrolPoints.Length)
        {
            spawnPointIndex = 0;
        }
        currentEnemyCount++;
        int index = Random.Range(0, tankSOList.tankSOList.Count);
        EnemyController controller = new(new EnemyModel(tankSOList.tankSOList[index]));
        enemyControllers.Add(controller);
        EnemyView enemyView = Instantiate(enemyPrefab, patrolPoints[spawnPointIndex].position, patrolPoints[spawnPointIndex].rotation);
        enemyView.SetController(controller);
        controller.SetTankView(enemyView);
    }

    public Transform[] GetPatrolPoints()
    {
        return patrolPoints;
    }

    private void OnEnemyDead()
    {
        currentEnemyCount--;
        Invoke(nameof(SpawnTank), spawnDelay);
    }

    public int GetEnemyCount { get { return currentEnemyCount; } }
    private void OnDisable()
    {
        EventHandler.Instance.OnEnemyDeath -= OnEnemyDead;
    }
}
