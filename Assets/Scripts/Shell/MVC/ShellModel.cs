using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellModel
{
    private ShellObject shellObject;
    private ShellController shellController;

    public ShellModel(ShellObject _object)
    {
        shellObject = _object;
    }

    public ShellObject GetShellObject { get { return shellObject; } }

    public void SetShellController(ShellController _controller)
    {
        shellController = _controller;
    }
}
