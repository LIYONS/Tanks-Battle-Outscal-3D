using System;
using UnityEngine;

public class EnemyController
{
    public EnemyModel enemyModel;

    public EnemyView enemyView;

    private TankScriptableObject enemyObject;
    private float currentHealth;
    private bool isDead;


    public EnemyController(EnemyModel _model)
    {
        this.enemyModel = _model;
        enemyObject = enemyModel.GetTankObject();
        currentHealth = enemyObject.maxHealth;
        isDead = false;
    }
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        enemyView.SetHealthUI(currentHealth);
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            OnDeath();
        }
    }
    private void OnDeath()
    {
        enemyView.OnDeath();
    }

    private void Destroy(GameObject gameObject)
    {
        throw new NotImplementedException();
    }

    public void SetTankView(EnemyView _enemyView)
    {
        enemyView = _enemyView;
    }
    public EnemyModel GetEnemyModel()
    {
        return enemyModel;
    }
}
