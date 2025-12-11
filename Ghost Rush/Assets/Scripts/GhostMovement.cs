using UnityEngine;
using System.Collections;

public class GhostMovement : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;
    public float dmg = 5f;
    public GameManager gmScript;
    private SpriteRenderer spriteRenderer;
    public float health = 50f;
    private bool suction = false;
    private bool cd = false;
    private Rigidbody2D rb;
    private Transform barFill;
    public float maxHealth = 50f;


    private Animator anim;

     void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
       // health = 50f;
       // maxHealth = health;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        barFill = transform.Find("GhostUI/Background/Fill");

    }

    public void TakeDamage(float amt)
    {
        health -= amt;
        //Update Health Bar
        if (barFill != null)
        {
            float ratio = Mathf.Clamp01(health / maxHealth);
            barFill.localScale = new Vector3(ratio, 1f, 1f);
        }
            if (health <= 0){
            Destroy(gameObject);
            Leveling.Instance.addSouls(1);
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
        rb.linearVelocity = direction * speed;
      
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
                    pm.TakeDamage(dmg);
                    anim.SetTrigger("attack");
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
