using UnityEngine;
using TankGame.Tanks.PlayerServices;
using TankGame.Tanks.EnemyServices;

namespace TankGame.Shell
{
    public class ShellController
    {
        private ShellModel shellModel;
        private ShellView shellView;

        public ShellController(ShellModel _model)
        {
            shellModel = _model;
        }
        public void Explode(Rigidbody rb)
        {
            if (rb.gameObject.CompareTag("Player"))
            {
                PlayerView playerTankView = rb.gameObject.GetComponent<PlayerView>();
                playerTankView.TakeDamage(CalculateDamage(rb.position));
            }
            EnemyView enemyTankView = rb.GetComponent<EnemyView>();
            if (enemyTankView)
            {
                float damage = CalculateDamage(rb.position);
                enemyTankView.TakeDamage(damage);
            }
        }
        float CalculateDamage(Vector3 targetPosition)
        {
            Vector3 explosionToTarget = targetPosition - shellView.transform.position;
            float explosionMagnitude = explosionToTarget.magnitude;
            float relativeDamage = (shellModel.GetShellObject.explosionRadius - explosionMagnitude) / shellModel.GetShellObject.explosionRadius;
            float damage = relativeDamage * shellModel.GetShellObject.maxDamage;
            damage = Mathf.Max(0, damage);
            return damage;
        }

        public void SetShellView(ShellView _view)
        {
            shellView = _view;
        }

        public ShellView GetShellView { get { return shellView; } }

        public ShellModel GetShellModel { get { return shellModel; } }
    }
}
