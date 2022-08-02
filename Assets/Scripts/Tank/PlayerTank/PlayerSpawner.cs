using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public PlayerTankView playerTankView;
    public TankScriptableObject playerObject;

    void OnEnable()
    {
        CreatePlayerTank();      
    }

    void CreatePlayerTank()
    {
        PlayerTankModel playerTankModel = new PlayerTankModel(playerObject);
        PlayerTankController playerTankController = new PlayerTankController(playerTankModel);
        playerTankView = Instantiate(playerTankView);
        playerTankView.SetComponents(playerTankController, playerObject);
        playerTankController.SetTankView(playerTankView);
    }
}
