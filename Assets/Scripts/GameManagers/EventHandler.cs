using System;

public sealed class EventHandler : MonoSingletonGeneric<EventHandler>
{
    public event Action<int> BulletAchievement;
    public event Action OnEnemyDeath;
    public event Action OnGameOver;
    public void InvokeBulletAchievement(int bulletCount)
    {
        BulletAchievement?.Invoke(bulletCount);
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
