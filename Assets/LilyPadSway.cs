using UnityEngine;

public class ContactPlayer : MonoBehaviour
{
    [Header("Sway Settings")]
    [SerializeField] private float swayAmount = 15f;
    [SerializeField] private float swaySpeed = 2f;
    [SerializeField] private float returnSpeed = 3f;
    [SerializeField] private float swayRadius = 1f;
    
    [Header("Detection")]
    [SerializeField] private LayerMask playerLayer; //set layer on player model
    [SerializeField] private string playerTag = "Player"; //player model's tag must == "Player"
    
    private Vector3 originalRotation;
    private Vector3 targetRotation;
    private Vector3 currentRotation;
    private Transform playerTransform;
    private bool isSwaying = false;

    void Start()
    {
        originalRotation = transform.localEulerAngles;
        currentRotation = originalRotation;
        targetRotation = originalRotation;
    }

    void Update()
    {
        Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, swayRadius, playerLayer);
    
        
        isSwaying = false;
        foreach (Collider col in nearbyObjects)
        {
            if (col.CompareTag(playerTag))
            {
                playerTransform = col.transform;
                isSwaying = true;
                break;
            }
        }

        if (isSwaying && playerTransform != null)
        {
            Vector3 dirFromPlayer = transform.position - playerTransform.position;
            dirFromPlayer.y = 0;
            dirFromPlayer.Normalize();

            float swayX = dirFromPlayer.z * swayAmount;
            float swayZ = -dirFromPlayer.x * swayAmount;
            
            targetRotation = originalRotation + new Vector3(swayX, 0, swayZ);
        }
        else
        {
            targetRotation = originalRotation;
        }

        float speed = isSwaying ? swaySpeed : returnSpeed;
        currentRotation = Vector3.Lerp(currentRotation, targetRotation, Time.deltaTime * speed);
        transform.localEulerAngles = currentRotation;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, swayRadius);
    }
}
