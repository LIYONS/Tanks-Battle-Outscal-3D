using UnityEngine;
using TankGame.GameManagers;
using TankGame.GlobalServices;

namespace TankGame.Shell
{
    public class ShellService : MonoSingletonGeneric<ShellService>
    {
        [SerializeField] private ShellView shellPrefab;

        private ShellServicePool shellServicePool;

        private void OnEnable()
        {
            shellServicePool = GetComponent<ShellServicePool>();
        }

        public void GetShell(ShellObject shellObject,Transform spawnPoint,Vector3 velocity)
        {
            ShellController shellController = shellServicePool.GetBullet(shellPrefab, shellObject);
            if (shellController != null)
            {
                ShellView shellView = shellController.GetShellView;
                PlayFireSound();
                shellView.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
                shellView.GetComponent<Rigidbody>().velocity = velocity;
            }
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
