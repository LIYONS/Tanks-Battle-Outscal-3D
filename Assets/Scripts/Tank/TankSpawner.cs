using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSpawner : MonoBehaviour
{
    //PlayerTank
    public PlayerTankView playerTankView;
    public Joystick joyStick;
    public TankScriptableObject playerObject;

    //EnemyTank
    public EnemyTankView enemyTankView;
    public int enemyTankCount;
    public TankList enemyObjects;

    void Start()
    {
        CreatePlayerTank();
        for (int i = 0; i < enemyTankCount; i++)
        {
           // CreateEnemyTank();
        }
        
    }

    void CreatePlayerTank()
    {
        PlayerTankModel playerTankModel = new PlayerTankModel(playerObject);
        PlayerTankController playerTankController = new PlayerTankController(playerTankModel);
        playerTankView = Instantiate(playerTankView);
        playerTankView.SetComponents(joyStick, playerTankController, playerObject);
        playerTankController.SetTankView(playerTankView);
    }

    void CreateEnemyTank()
    {
        int index = Random.Range(0, enemyObjects.tankList.Length);
        EnemyTankModel model = new EnemyTankModel(enemyObjects.tankList[index]);
        EnemyTankController controller = new EnemyTankController(model);
        enemyTankView = Instantiate(enemyTankView);
        enemyTankView.SetComponents(controller, enemyObjects.tankList[index]);
        controller.SetTankView(enemyTankView);
    }

}
