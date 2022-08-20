using UnityEngine;
using System.Collections.Generic;

public class PlayerService : MonoSingletonGeneric<PlayerService>
{
    [SerializeField] private PlayerView playerView;
    [SerializeField] private TankList tankList;
    [SerializeField] private TankType tankType;

    private TankScriptableObject playerObject;
    private PlayerController playerController;
    private PlayerModel playerModel;
    void Awake()
    {
        CreatePlayerTank();      
    }

    void CreatePlayerTank()
    {
        playerObject = tankList.tankSOList.Find(i => i.tankType == tankType);
        if (playerObject)
        {
            playerModel = new PlayerModel(playerObject);
            playerController = new PlayerController(playerModel);
            playerModel.SetTankController(playerController);
            GenerateTankView();
        }
    }

    private void GenerateTankView()
    {
        playerView = Instantiate(playerView);
        playerView.SetController(playerController);
        playerController.SetPlayerView(playerView);
    }

    public TankScriptableObject GetPlayerObject()
    {
        return playerObject;
    }
}
