using UnityEngine;

public class TriggerCollide : MonoBehaviour
{
   void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Ghost"))
        {
            GhostMovement ghostControl = other.GetComponent<GhostMovement>();
            if (ghostControl != null)
            {
                ghostControl.stopMovement(true);
                ghostControl.TakeDamage(1);
            
            }
        }
    }   

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ghost"))
        {
            other.GetComponent<GhostMovement>()?.stopMovement(false);
        }
    }

}
