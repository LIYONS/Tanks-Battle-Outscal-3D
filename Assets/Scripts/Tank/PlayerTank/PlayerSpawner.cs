using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] PlayerTankView playerTankView;
    [SerializeField] TankScriptableObject playerObject;
    void Awake()
    {
        CreatePlayerTank();      
    }

    void CreatePlayerTank()
    {
        PlayerTankModel playerTankModel = new PlayerTankModel(playerObject);
        PlayerTankController playerTankController = new PlayerTankController(playerTankModel);
        playerTankView = Instantiate(playerTankView);
        playerTankView.SetComponents(playerTankController);
        playerTankController.SetTankView(playerTankView);
    }
}
