using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingletongeneric<T>: MonoBehaviour where T:MonoSingletongeneric<T>
{
    private static T instance;

    public static T Instance { get { return instance; } }

    private void Awake()
    {
        if(!instance)
        {
            instance = (T)this;
        }
        else
        {
            Debug.LogError("Duplicate Singleton");
            Destroy(this);
        }
    }
}
