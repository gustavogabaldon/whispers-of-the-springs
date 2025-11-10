using UnityEngine;

public class KeyboardXRMovement : MonoBehaviour
{
    public float speed = 2f;      // Horizontal movement speed
    public float verticalSpeed = 2f; // Vertical movement speed

    void Update()
    {
        // Get horizontal and vertical input
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float vertical = Input.GetAxis("Vertical");     // W/S or Up/Down
        float upDown = 0f;

        // Q for down, E for up
        if (Input.GetKey(KeyCode.E)) upDown = 1f;
        if (Input.GetKey(KeyCode.Q)) upDown = -1f;

        // Create movement vector relative to XR Rig orientation
        Vector3 move = new Vector3(horizontal, upDown * verticalSpeed, vertical);
        move = transform.TransformDirection(move);

        // Apply movement
        transform.position += move * speed * Time.deltaTime;
    }
}
