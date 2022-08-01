using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public PlayerTankView playerTankView;
    public Joystick joyStick;
    public TankScriptableObject playerObject;

    void Start()
    {
        CreatePlayerTank();      
    }

    void CreatePlayerTank()
    {
        PlayerTankModel playerTankModel = new PlayerTankModel(playerObject);
        PlayerTankController playerTankController = new PlayerTankController(playerTankModel);
        playerTankView = Instantiate(playerTankView);
        playerTankView.SetComponents(joyStick, playerTankController, playerObject);
        playerTankController.SetTankView(playerTankView);
    }
}
