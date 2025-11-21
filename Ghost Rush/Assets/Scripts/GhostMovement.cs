using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;
    private SpriteRenderer spriteRenderer;
    public float health = 50f;
    private bool suction = false;

     void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = 50f;
    }

    public void TakeDamage(int amt)
    {
        health -= amt;
        if(health <= 0){
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (suction) return;
        Vector2 currentPos = transform.position;
        Vector2 targetPos = player.position;
        if (targetPos.x < currentPos.x)
            spriteRenderer.flipX = false;   // moving left
         else
            spriteRenderer.flipX = true;  // moving right
        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            speed * Time.deltaTime
        );
    }

    public void stopMovement(bool tf)
    {
        suction = tf;

        if (suction)
        {
           transform.position = transform.position;
        }
    }
}
