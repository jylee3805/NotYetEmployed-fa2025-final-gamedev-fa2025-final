using UnityEngine;
using System.Collections;
public class VacuumGun : MonoBehaviour
{
    [Header("Assign the hitbox object here")]
    public GameObject hitbox;

    public int level = 1;

    public float currentCharge = 150f;

    public float maxCharge = 150f;
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

    public float Range => baseRange * Mathf.Pow(1f + rangeGrowth, level - 1);
    public float Wideness => baseWide * Mathf.Pow(1f + wideGrowth, level - 1);
  
    public float rechargeDelay = 1.5f;   
    public float rechargeRate = 40f;   
    public float reduceRate = 30f;   

    private float timeSinceLastInput = 0f;
    private bool isHolding = false;

    void Update()
    {
        if (Input.GetMouseButton(0) && currentCharge > 0)
        {
            isHolding = true;
            timeSinceLastInput = 0f;

            currentCharge -= reduceRate * Time.deltaTime;
            currentCharge = Mathf.Clamp(currentCharge, 0, maxCharge);

            if (!hitbox.activeSelf)
                hitbox.SetActive(true);
        }
        else
        {
            isHolding = false;

            if (hitbox.activeSelf)
                hitbox.SetActive(false);

            if (currentCharge < maxCharge)
                timeSinceLastInput += Time.deltaTime;
        }

  
        if (!isHolding && currentCharge < maxCharge && timeSinceLastInput >= rechargeDelay)
        {
            currentCharge += rechargeRate * Time.deltaTime;
            currentCharge = Mathf.Clamp(currentCharge, 0, maxCharge);
        }
    }


    public void LevelUp(int globalLevel)
    {
        //Trigger Popup Change then update hitbox
        level = globalLevel;
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