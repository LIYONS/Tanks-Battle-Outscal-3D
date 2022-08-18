using System;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerTankController
{
    private PlayerTankModel tankModel;
    private PlayerTankView tankView;
    private TankScriptableObject tankObject;
    private Rigidbody rb;
    private float currentHealth;
    private bool isDead;
    public PlayerTankController(PlayerTankModel _model)
    {
        tankModel = _model;
        tankModel.SetTankController(this);
        tankObject = tankModel.GetTankObject();
        currentHealth = tankObject.maxHealth;
        isDead = false;
    }

    public void Movement(float movementInput)
    {
        Vector3 movement = tankView.gameObject.transform.forward * movementInput * tankObject.movementSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }

    public void Rotate(float turnInput)
    {   
        float turn = turnInput * 0.5f * tankObject.turnSpeed * Time.deltaTime;
        Quaternion turnValue = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnValue);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        tankView.SetHealthUI(currentHealth);
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            OnDeath();
        }
    }
    async private void OnDeath()
    {
        await DestroyAllEnemies();
        await Task.Delay(TimeSpan.FromSeconds(1f));
        await DestroyLevel();
        tankView.OnDeath();
    }
    async Task DestroyAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            EnemyTankView enemyTankView = enemies[i].GetComponent<EnemyTankView>();
            enemyTankView.TakeDamage(tankObject.maxHealth);
        }
        await Task.Yield();
    }
    async Task DestroyLevel()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Level");
        for (int i = 0; i < gameObjects.Length; i++)
        {
            await Task.Delay(TimeSpan.FromSeconds(.2f));
            gameObjects[i].SetActive(false);
        }
        await Task.Yield();
    }
    public PlayerTankModel GetTankModel()
    {
        return tankModel;
    }
    public PlayerTankView GetTankView()
    {
        return tankView;
    }

    public void SetTankView(PlayerTankView _tankView)
    {
        tankView = _tankView;
        rb = tankView.GetRigidBody();
    }
}
