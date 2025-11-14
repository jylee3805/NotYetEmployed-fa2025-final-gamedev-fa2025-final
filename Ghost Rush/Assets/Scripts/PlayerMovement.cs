using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 input;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get WASD input
        input.x = Input.GetAxisRaw("Horizontal"); // A/D
        input.y = Input.GetAxisRaw("Vertical");   // W/S
        input.Normalize(); // prevent faster diagonal movement
    }

    void FixedUpdate()
    {
        rb.linearVelocity = input * moveSpeed;
    }
}