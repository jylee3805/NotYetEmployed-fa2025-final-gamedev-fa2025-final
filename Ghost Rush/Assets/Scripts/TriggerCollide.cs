using UnityEngine;

public class TriggerCollide : MonoBehaviour
{
    public Sprite normalSprite;
    public Sprite suckedSprite;

   void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Ghost"))
        {
            GhostMovement ghostControl = other.GetComponent<GhostMovement>();
            if (ghostControl != null)
            {
                ghostControl.stopMovement(true);
                ghostControl.TakeDamage(VacuumGun.Instance.Damage);
                //SpriteRenderer spriteRenderer = other.GetComponentInChildren<SpriteRenderer>();
                //spriteRenderer.sprite = suckedSprite;
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
