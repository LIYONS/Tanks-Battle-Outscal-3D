using UnityEngine;

public class TankController
{
    private TankModel tankModel;

    private TankView tankView;

    public TankController(TankView view,TankModel model)
    {
        tankModel = model;
        tankView = view;

        GameObject.Instantiate(tankView.gameObject);

        tankView.SetTankController(this);
        tankModel.SetTankController(this);
    }
}
