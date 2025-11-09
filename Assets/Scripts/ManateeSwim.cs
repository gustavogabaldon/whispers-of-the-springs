using UnityEngine;

public class ManateeSwim : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 0.7f;

    [Tooltip("If true, rotates the model so forward motion goes the correct direction.")]
    public bool invertForward = true;

    [Header("Swimming Bounds")]
    public Transform center; 
    public Vector2 xzHalfExtents = new Vector2(40f, 40f);
    public float yMin = -6f;
    public float yMax = -1.5f;

    private Vector3 targetDirection;
    private float turnSpeed = 0.5f;

    void Start()
    {
        PickNewDirection();
    }

    void Update()
    {
        // Forward movement
        transform.position += transform.forward * speed * Time.deltaTime;

        // Smooth turn toward target direction
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

        // Stay within Y bounds
        Vector3 pos = transform.position;
        pos.y = Mathf.Clamp(pos.y, yMin, yMax);
        transform.position = pos;

        // Occasionally change direction
        if (Random.value < 0.005f)
            PickNewDirection();
    }

    void PickNewDirection()
    {
        // Pick random direction
        targetDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-0.3f, 0.3f), Random.Range(-1f, 1f)).normalized;

        // Flip forward direction for your model orientation
        if (invertForward)
            targetDirection = -targetDirection;
    }
}
