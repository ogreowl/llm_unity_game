using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
{
    public float speed = 5.0f; // Movement speed
    public float rotationSpeed = 720.0f; // Degrees per second
    public float groundOffset = 0.1f; // Offset from the ground to keep the player slightly above the terrain

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float x = 0;
        float z = 0;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            x = 1 * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            x = -1 * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            z = 1 * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            z = -1 * speed * Time.deltaTime;
        }

        Vector3 movement = new Vector3(x, 0, z);
        transform.Translate(movement, Space.World);

        if (x != 0 || z != 0) // Check if there is any movement
        {
            // Set rotation to face the direction of movement
            transform.rotation = Quaternion.LookRotation(movement);
        }

        // Adjust height based on terrain
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.down, out hit, 2f))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y + groundOffset, transform.position.z);
        }

        // Animation control
        bool isMoving = (x != 0 || z != 0);
        animator.SetBool("IsMoving", isMoving);
    }
}