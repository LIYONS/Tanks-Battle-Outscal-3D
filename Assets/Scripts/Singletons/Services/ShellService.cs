using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        ShellView shellView = shellController.GetShellView;
        PlayFireSound();
        return shellView.GetComponent<Rigidbody>();
    }

    private void PlayFireSound()
    {
        var instance = AudioManager.Instance;
        if (instance)
        {
            instance.PlaySfx(SoundType.Fire);
        }
    }
    public void ReturnToPool(ShellController shellController)
    {
        shellServicePool.ReturnItem(shellController);
    }
}