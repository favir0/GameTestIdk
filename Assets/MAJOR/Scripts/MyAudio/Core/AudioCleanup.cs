using UnityEngine;

namespace MyAudio.Core
{
    public class AudioCleanup : MonoBehaviour
    {
        public static void Cleanup()
        {
            AudioSource[] soundObjects = FindObjectsOfType<AudioSource>();
            foreach (var obj in soundObjects)
            {
                Destroy(obj);
            }
        }
    }
}
