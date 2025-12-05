using UnityEngine;

public class VacuumGun : MonoBehaviour
{
    [Header("Assign the hitbox object here")]
    public GameObject hitbox;

    public Vector3 BaseScale = new Vector3(1.5f, 1.7f, 0.7f);

    [Header("Base Stats")]
    public float baseRange = 1f;
    public float baseWide = 1f;

    [Header("Current Stats")]
    public float currentRange = 1f;
    public float currentWide = 1f;

    [Header("Leveling Stats")]
    public float rangeGrowth = 0.05f;
    public float wideGrowth = 0.05f;

    public float Range => baseRange * Mathf.Pow(1f + rangeGrowth, currentRange - 1);
    public float Wideness => baseWide * Mathf.Pow(1f + wideGrowth, currentWide - 1);

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
    
    public void LevelUp(int globalLevel)
    {
        //Trigger Popup Change then update hitbox
        UpdateHitbox();
    }

    private void UpdateHitbox()
    {
        if(hitbox == null)
        {
            return;
        }

        hitbox.transform.localScale = new Vector3(
            BaseScale.x * Wideness,
            BaseScale.y * Wideness,
            BaseScale.z * Range
        );

        hitbox.transform.localPosition = new Vector3(
           hitbox.transform.localPosition.x,
           hitbox.transform.localPosition.y,
           (BaseScale.z * 0.5f) * Range
       );
    }
}