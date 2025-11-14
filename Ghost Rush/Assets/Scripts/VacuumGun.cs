using UnityEngine;

public class VacuumGun : MonoBehaviour
{
    [Header("Assign the hitbox object here")]
    public GameObject hitbox;

    void Update()
    {
        if (Input.GetMouseButton(0))       // left mouse held
        {
            if (!hitbox.activeSelf)
                hitbox.SetActive(true);
        }
        else
        {
            if (hitbox.activeSelf)
                hitbox.SetActive(false);
        }
    }
}