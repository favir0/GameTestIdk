using UnityEngine;

public class PersistentObject : MonoBehaviour
{
    private void Awake()
    {
        // Убедимся, что объект не будет уничтожен при загрузке новой сцены
        DontDestroyOnLoad(gameObject);

        // Если есть другие объекты с этим скриптом, уничтожаем их
        PersistentObject[] objs = FindObjectsOfType<PersistentObject>();
        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }
    }
}