using UnityEngine;

namespace MyAudio.Core
{
    public abstract class Singleton<T> : MonoBehaviour where T: Singleton<T>
    {
        protected static T instance;

        protected void InitSingleton (T newInstance) 
        {
            if (instance != null) 
                throw new UnityException($"There should be only one {typeof(T)} in the scene");

            instance = newInstance;
        }

        protected void ClearSingleton () 
        {
            instance = null;
        }
    }
}
