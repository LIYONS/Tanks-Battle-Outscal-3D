using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame.GlobalServices
{
    public class MonoSingletonGeneric<T> : MonoBehaviour where T : MonoSingletonGeneric<T>
    {
        private static T instance;

        public static T Instance { get { return instance; } }

        protected virtual void Awake()
        {
            if (!instance)
            {
                instance = (T)this;
                DontDestroyOnLoad((T)this);
            }
            else
            {
                Destroy(this);
            }
        }
    }
}
