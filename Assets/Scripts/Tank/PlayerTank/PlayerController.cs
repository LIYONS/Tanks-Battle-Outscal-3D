using System;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerController
{
    private PlayerModel playerModel;
    private PlayerView playerView;
    private TankScriptableObject tankObject;
    private Rigidbody rb;
    private float currentHealth;
    private bool isDead;
    public PlayerController(PlayerModel _model)
    {
        playerModel = _model;
        tankObject = playerModel.GetTankObject();
        currentHealth = tankObject.maxHealth;
        isDead = false;
    }


    public void Movement(float movementInput)
    {
        if(!rb)
        {
            rb = playerView.GetRigidBody();
        }
        Vector3 movement = movementInput * tankObject.movementSpeed * Time.deltaTime * playerView.gameObject.transform.forward;
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
        playerView.SetHealthUI(currentHealth);
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
        playerView.OnDeath();
    }
    async Task DestroyAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            EnemyView enemyTankView = enemies[i].GetComponent<EnemyView>();
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
    public PlayerModel GetPlayerModel()
    {
        return playerModel;
    }

    public void SetPlayerView(PlayerView _view)
    {
        playerView = _view;
    }
}