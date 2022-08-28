using UnityEngine;
using TankGame.GameManagers;
using TankGame.GlobalServices;

namespace TankGame.Shell
{
    public class ShellService : MonoSingletonGeneric<ShellService>
    {
        [SerializeField] ShellView shellPrefab;
        private ShellServicePool shellServicePool;

        private void OnEnable()
        {
            shellServicePool = GetComponent<ShellServicePool>();
        }

        public Rigidbody GetShell(ShellObject shellObject)
        {
            ShellController shellController = shellServicePool.GetBullet(shellPrefab, shellObject);
            if (shellController != null)
            {
                ShellView shellView = shellController.GetShellView;
                PlayFireSound();
                return shellView.GetComponent<Rigidbody>();
            }
            return null;
        }

        private void PlayFireSound()
        {
            var instance = AudioManager.Instance;
            if (instance)
            {
                instance.PlaySound(SoundType.Fire);
            }
        }
        public void ReturnToPool(ShellController shellController)
        {
            shellServicePool.ReturnItem(shellController);
        }
    }
}
