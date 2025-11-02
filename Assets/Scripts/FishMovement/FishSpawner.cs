using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject[] fishPrefabs;
    public int fishCount = 30;
    public Vector3 spawnArea = new Vector3(40, 10, 40);
    public bool randomRotation = true;

    [Header("Fish Behavior")]
    public float minSpeed = 1f;
    public float maxSpeed = 3f;

    [Header("Depth Range")]
    public float minY = -9f;  // bottom limit
    public float maxY = -1f;  // surface limit

    void Start()
    {
        for (int i = 0; i < fishCount; i++)
        {
            SpawnFish();
        }
    }

    void SpawnFish()
    {
        if (fishPrefabs.Length == 0) return;

        // Random position within area + fixed vertical range
        Vector3 pos = new Vector3(
            transform.position.x + Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
            Random.Range(minY, maxY),
            transform.position.z + Random.Range(-spawnArea.z / 2, spawnArea.z / 2)
        );

        GameObject prefab = fishPrefabs[Random.Range(0, fishPrefabs.Length)];
        GameObject fish = Instantiate(prefab, pos, Quaternion.identity);

        if (randomRotation)
            fish.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

        // Assign random speed + Y limits
        FishMovement mover = fish.GetComponent<FishMovement>();
        if (mover != null)
        {
            mover.speed = Random.Range(minSpeed, maxSpeed);
            mover.minY = minY;
            mover.maxY = maxY;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 0.5f, 1, 0.2f);
        Gizmos.DrawCube(transform.position, spawnArea);
    }
}
