using UnityEngine;

public class ManateeSpawnerSimple : MonoBehaviour
{
    public GameObject manateePrefab;
    public int count = 5;
    public float spacing = 3f;
    public float depth = -4f;

    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 pos = new Vector3(i * spacing, depth, 0);
            Instantiate(manateePrefab, pos, Quaternion.identity);
        }
    }
}
