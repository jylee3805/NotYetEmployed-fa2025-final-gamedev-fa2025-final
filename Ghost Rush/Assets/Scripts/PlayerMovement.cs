using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 input;
    private Transform character;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        character = transform.Find("Character");
        spriteRenderer = character.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Get WASD input
        input.x = Input.GetAxisRaw("Horizontal"); // A/D
        input.y = Input.GetAxisRaw("Vertical");   // W/S
        input.Normalize(); // prevent faster diagonal movement

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (spriteRenderer.flipX)
                spriteRenderer.flipX = !spriteRenderer.flipX;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (!spriteRenderer.flipX)
                spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = input * moveSpeed;
    }
}