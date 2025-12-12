using UnityEngine;

public class TriggerCollide : MonoBehaviour
{
    public Sprite normalSprite;
    public Sprite suckedSprite;

    public Transform pos;

    public VacuumGun vacuumType;

   void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Ghost"))
        {
            if (vacuumType.currentVac == Vacuum.Wall)
            {
                GhostMovement ghostControl = other.GetComponent<GhostMovement>();
                if (ghostControl != null)
                {
                    ghostControl.stopMovement(true);
                    ghostControl.TakeDamage(VacuumGun.Instance.Damage);
                }
            }
            else
            {
                Vector2 playerPos = transform.position;
                Vector2 ghostPos = other.transform.position;

                RaycastHit2D hit = Physics2D.Linecast(pos.position, ghostPos, LayerMask.GetMask("IgnoreGhosts"));

                if (hit.collider == null) 
                {
                    GhostMovement ghostControl = other.GetComponent<GhostMovement>();
                    if (ghostControl != null)
                    {
                        ghostControl.stopMovement(true);
                        ghostControl.TakeDamage(VacuumGun.Instance.Damage);
                    }
                }
                else
                {
                
                } 
            }
           
        }
    }   

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ghost"))
        {
            other.GetComponent<GhostMovement>()?.stopMovement(false);
           // SpriteRenderer spriteRenderer = other.GetComponentInChildren<SpriteRenderer>();
           // spriteRenderer.sprite = normalSprite;
        }
    }

}
