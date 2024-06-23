using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab; // The prefab to spawn
    public Transform spawnArea; // The area within which the prefab can be spawned
    public float spawnInterval = 2f; // Time between spawns

    private void Start()
    {
        InvokeRepeating("SpawnPrefab", 0f, spawnInterval);
    }

    private void SpawnPrefab()
    {
        Vector3 randomPosition = GetRandomPositionWithinArea();
        Instantiate(prefab, randomPosition, Quaternion.identity, transform);
    }

    private Vector3 GetRandomPositionWithinArea()
    {
        if (spawnArea == null) return Vector3.zero;

        Vector3 min = spawnArea.position - spawnArea.localScale / 2;
        Vector3 max = spawnArea.position + spawnArea.localScale / 2;

        float x = Random.Range(min.x, max.x);
        float y = Random.Range(min.y, max.y);
        float z = Random.Range(min.z, max.z);

        return new Vector3(x, y, z);
    }
}