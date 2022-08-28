using System;
using System.Threading.Tasks;
using UnityEngine;
using TankGame.GameManagers;
using TankGame.Tanks.EnemyServices;
using TankGame.Shell;
using System.Collections.Generic;

namespace TankGame.Tanks.PlayerServices
{
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
            if (!rb)
            {
                rb = playerView.GetRigidBody;
            }
            Vector3 movement = movementInput * tankObject.movementSpeed * Time.deltaTime * playerView.gameObject.transform.forward;
            rb.MovePosition(rb.position + movement);
        }

        public void Rotate(float turnInput)
        {
            if (!rb)
            {
                rb = playerView.GetRigidBody;
            }
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
                EventManager.Instance.InvokeOnGameOver();
                isDead = true;
                OnDeath();
            }
        }
        async private void OnDeath()
        {
            await DestroyAllEnemies();
            await Task.Delay(TimeSpan.FromSeconds(1f));
        }
        async private Task DestroyAllEnemies()
        {
            List<EnemyController> enemies = EnemyService.Instance.GetEnemies();
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].OnDeath();
            }
            await Task.Yield();
        }

        public void Fire(Vector3 velocity)
        {
            EventManager.Instance.InvokeOnBulletFired();
            ShellService.Instance.GetShell(playerView.GetShellObject,playerView.GetFireTransform,velocity);
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
}