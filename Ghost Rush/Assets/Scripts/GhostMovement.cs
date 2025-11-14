using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;

    void Update()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            speed * Time.deltaTime
        );
    }
}
