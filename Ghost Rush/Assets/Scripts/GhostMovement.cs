using UnityEngine;
using System.Collections;

public class GhostMovement : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;
    public GameManager gmScript;
    private SpriteRenderer spriteRenderer;
    public float health = 50f;
    private bool suction = false;
    private bool cd = false;
    private Rigidbody2D rb;
    
    private Animator anim;

     void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = 50f;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int amt)
    {
        health -= amt;
        if(health <= 0){
            Destroy(gameObject);
            gmScript.ghostDeath();
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
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * 3;
      
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           if (!cd)
            {
       
                PlayerMovement pm = other.transform.parent.GetComponent<PlayerMovement>();

                if (pm != null)
                {
                    pm.TakeDamage(5);
                    StartCoroutine(attackCD());
                }
            }
            
        }
    }   

    public void stopMovement(bool tf)
    {
        anim.SetBool("isSucked",tf);
        suction = tf;
        if (suction)
        {
           rb.linearVelocity = Vector2.zero;
        }
    }

     IEnumerator attackCD()
    {
        cd = true;
        yield return new WaitForSeconds(2f);
        cd = false;
    }
}
