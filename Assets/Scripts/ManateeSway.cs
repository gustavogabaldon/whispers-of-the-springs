using UnityEngine;

public class ManateeSway : MonoBehaviour
{
    public Transform tail;
    public float speed = 0.5f;
    public float angle = 10f;

    void Update()
    {
        float t = Mathf.Sin(Time.time * speed) * angle;
        tail.localRotation = Quaternion.Euler(t, 0, 0);
    }
}
