using UnityEngine;

public class TriggerCollide : MonoBehaviour
{
   void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Ghost"))
    {
        Destroy(other.gameObject); 
    }
}
}
