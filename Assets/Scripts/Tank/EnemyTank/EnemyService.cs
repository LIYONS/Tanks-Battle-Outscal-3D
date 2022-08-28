using UnityEngine;
using System.Collections.Generic;
using TankGame.GameManagers;
using TankGame.GlobalServices;

namespace TankGame.Tanks.EnemyServices
{
    public class EnemyService : MonoSingletonGeneric<EnemyService>
    {
        [SerializeField] private EnemyView enemyPrefab;
        [SerializeField] private TankSOList tankSOList;
        [SerializeField] private int numberOfEnemies;
        [SerializeField] private Transform[] patrolPoints;
        [SerializeField] private float timeBtwSpawns;
        [SerializeField] private float spawnDelay;

        private float timer;
        private int spawnPointIndex;
        private List<EnemyController> enemyControllers = new();

        protected override void Awake()
        {
            base.Awake();
            timer = timeBtwSpawns;
            spawnPointIndex = 0;
            for (int i = 0; i < numberOfEnemies; i++)
            {
                SpawnTank();
            }
        }
        private void Start()
        {
            
        }
        private void OnEnable()
        {
            EventManager.Instance.OnEnemyDeath += OnEnemyDead;
            EventManager.Instance.OnGameOver += GameOver;
        }
        private void Update()
        {
            if (timer < Time.time)
            {
                SpawnTank();
                timer = Time.time + timeBtwSpawns;
            }
        }
        private void SpawnTank()
        {
            if (spawnPointIndex == patrolPoints.Length - 1)
            {
                spawnPointIndex = 0;
            }
            spawnPointIndex++;
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
            Invoke(nameof(SpawnTank), spawnDelay);
        }

        private void GameOver()
        {
            enemyControllers.Clear();
        }
        private void OnDisable()
        {
            EventManager.Instance.OnEnemyDeath -= OnEnemyDead;
            EventManager.Instance.OnGameOver -= GameOver;
        }

        public void RemoveEnemy(EnemyController controller)
        {
            _ = enemyControllers.Remove(controller);
        }

        public List<EnemyController> GetEnemies()
        {
            return enemyControllers;
        }
    }
}
