using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellServicePool : GenericPool<ShellController>
{
    private ShellView shellPrefab;
    private ShellObject shellObject;

    private ShellController shellController;
    private ShellModel shellModel;
    private ShellView shellView;
    public ShellController GetBullet(ShellView _shellPrefab,ShellObject _shellObject)
    {
        shellPrefab = _shellPrefab;
        shellObject = _shellObject;
        shellController = GetItem();
        if (shellController != null && shellController.GetShellView !=null)
        {
            shellController.GetShellView.gameObject.SetActive(true);
            return shellController;
        }
        return null;
    }
    public override ShellController CreateItem()
    {
        shellModel = new ShellModel(shellObject);
        shellController = new ShellController(shellModel); 
        shellView = Instantiate(shellPrefab);
        SetReferences();
        return shellController;
    }

    private void SetReferences()
    {
        shellModel.SetShellController(shellController);
        shellView.SetShellController(shellController);
        shellController.SetShellView(shellView);
    }
}
