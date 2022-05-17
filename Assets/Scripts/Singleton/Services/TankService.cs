using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankService : MonoSingleton<TankService>
{
    [SerializeField] float movementSpeed;
    [SerializeField] float turnSpeed;
    public TankView tankView;
    private void Start()
    {
        CreateTank();
    }
    private void CreateTank()
    {
        TankModel tankModel = new TankModel(movementSpeed,turnSpeed);
        TankController tankController = new TankController(tankView, tankModel);

    }
}
