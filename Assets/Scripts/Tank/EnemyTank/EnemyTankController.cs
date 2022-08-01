using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankController : MonoBehaviour
{
    public EnemyTankModel model;

    public EnemyTankView view;

    Rigidbody rb;

    public EnemyTankController(EnemyTankModel _model)
    {
        this.model = _model;
    }

    public EnemyTankModel GetTankModel()
    {
        return model;
    }
    public EnemyTankView GetTankView()
    {
        return view;
    }

    public void SetTankView(EnemyTankView _tankView)
    {
        view = _tankView;
        rb = view.GetRigidBody();
    }
}
