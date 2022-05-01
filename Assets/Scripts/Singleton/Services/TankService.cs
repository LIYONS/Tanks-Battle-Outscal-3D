using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankService : MonoSingleton<TankService>
{
    public TankView tankView;
    private void Start()
    {
        CreateTank();
    }
    private void CreateTank()
    {
        TankModel tankModel = new TankModel();
        TankController tankController = new TankController(tankView, tankModel);

    }
}
