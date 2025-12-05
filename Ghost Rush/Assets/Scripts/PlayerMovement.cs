using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 input;
    private Transform character;
    private SpriteRenderer spriteRenderer;
    public float health = 100f;
    public float maxHealth = 100f;
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

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health<=0){
            SceneManager.LoadScene("GameOver");
        }

    }

    void FixedUpdate()
    {
        rb.linearVelocity = input * moveSpeed;
    }
}