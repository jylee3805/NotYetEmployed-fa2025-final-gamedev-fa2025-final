using UnityEngine;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;
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

    public float Damage { get; set; } = 1;

    //First val is x, second val is y, third value is the val to move the x to (pos), 4th val is val to move y to (pos)

    public List<Vector4> ScaleValues;
    public static VacuumGun Instance { get; private set; }

    private void Awake()
    {
       if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
       Instance = this;
    }
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


    public void LevelUpScale(int level)
    {
        Vector4 vector = ScaleValues[level - 1];
        hitbox.transform.localScale = new Vector2(vector.x, vector.y);
        hitbox.transform.localPosition = new Vector2(
            vector.z,
            vector.w
        );
    }

    public void LevelUpDamage()
    {
        Damage += 0.25f;
    }
}