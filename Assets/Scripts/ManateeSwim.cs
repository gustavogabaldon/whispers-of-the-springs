using UnityEngine;

public class ManateeSwim : MonoBehaviour
{
    public Vector3 center = Vector3.zero;
    public float radius = 6f;
    public float speed = 0.3f;
    public float verticalOffset = -4f;

    void Update()
    {
        float x = Mathf.Sin(Time.time * speed) * radius;
        float z = Mathf.Cos(Time.time * speed) * radius;
        transform.position = center + new Vector3(x, verticalOffset, z);
        transform.LookAt(center);
    }
}
