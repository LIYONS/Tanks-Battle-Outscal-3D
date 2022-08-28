using System;
using TankGame.GlobalServices;

namespace TankGame.GameManagers
{
    public sealed class EventManager : MonoSingletonGeneric<EventManager>
    {
        public event Action OnBulletFired;
        public event Action OnEnemyDeath;
        public event Action OnGameOver;
        public void InvokeOnBulletFired()
        {
            OnBulletFired?.Invoke();
        }

        public void InvokeEnemyDeath()
        {
            OnEnemyDeath?.Invoke();
        }

        public void InvokeOnGameOver()
        {
            OnGameOver?.Invoke();
        }
    }
}
