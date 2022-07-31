using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTankSpawner : MonoBehaviour
{
    public TankView tankView;
    public Joystick joyStick;

    public TankScriptableObject tankScriptableObject;
    void Start()
    {
        CreateTank();
    }

    void CreateTank()
    {
        TankModel tankModel = new TankModel(tankScriptableObject);
        TankController tankController = new TankController(tankModel);
        tankView = GameObject.Instantiate<TankView>(tankView);
        tankView.SetComponents(joyStick, tankController, tankScriptableObject);
        tankController.SetTankView(tankView);
    }

}
