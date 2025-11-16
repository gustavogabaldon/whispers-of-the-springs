using UnityEngine;

public class FishMovement : MonoBehaviour
{
    [Header("Fish Movement Settings")]
    public float speed = 2f;
    public float turnSpeed = 2f;
    public float swimRadius = 10f;

    [HideInInspector] public float minY = -9f; // bottom limit
    [HideInInspector] public float maxY = -1f; // surface limit

    private Vector3 target;

    void Start()
    {
        PickNewTarget();
    }

    void Update()
    {
        // Move forward
        transform.position += -transform.forward * speed * Time.deltaTime;


        // Rotate toward target
        Vector3 dir = (target - transform.position).normalized;
        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, turnSpeed * Time.deltaTime);

        // Choose new target if close enough
        if (Vector3.Distance(transform.position, target) < 1f)
            PickNewTarget();

        // Keep fish within vertical range
        Vector3 pos = transform.position;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }

    void PickNewTarget()
    {
        Vector3 center = transform.position;

        target = center + new Vector3(
            Random.Range(-swimRadius, swimRadius),
            Random.Range(-2f, 2f),
            Random.Range(-swimRadius, swimRadius)
        );

        // Clamp target within depth limits
        target.y = Mathf.Clamp(target.y, minY, maxY);
    }
}
