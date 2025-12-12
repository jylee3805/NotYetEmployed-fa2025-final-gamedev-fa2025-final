using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Leveling : MonoBehaviour
{
    public VacuumGun vacuumGun;
    public PlayerMovement playerMovement;

    public int HealthLevel { get; set; } = 1;
    public int SpeedLevel { get; set; } = 1;

    public int DamageLevel { get; set; } = 1;
    public int VaccumScaleLevel { get; set; } = 1;

    public int Souls { get; private set; }
    public static Leveling Instance { get; private set; }

    public List<Vacuum> unlockedVacuums { get; set; } = new List<Vacuum>() { Vacuum.Default };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
       if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addSouls(int souls)
    {
        this.Souls += souls;
    }

    public void UpgradeHealth()
    {
        if (HealthLevel >= 5)
        {
            return;
        }
        int soulsNeeded = 10 + HealthLevel * 2;
        if(Souls < soulsNeeded)
        {
            return;
        }
        Souls -= soulsNeeded;
        playerMovement.LevelUpHealth();
        HealthLevel += 1;
    }

    public void UpgradeSpeed()
    {
        if(SpeedLevel >= 5)
        {
            return;
        }
        int soulsNeeded = 10 + SpeedLevel * 2; 
        if(Souls < soulsNeeded)
        {
            return;
        }
        Souls -= soulsNeeded;
        playerMovement.LevelUpMoveSpeed();
        SpeedLevel += 1;
    }

    public void UpgradeDamage()
    {
        if(DamageLevel >= 5){
            return;
        }
        int soulsNeeded = 10 + DamageLevel * 2;
        if(Souls < soulsNeeded)
        {
            return;
        }
        Souls -= soulsNeeded;
        vacuumGun.LevelUpDamage();
        DamageLevel += 1;
        
    }

    public void UpgradeVaccumScale()
    {
        if (VaccumScaleLevel >= 5)
        {
            return;
        }
        int soulsNeeded = VaccumScaleLevel * 2;
        if (Souls < soulsNeeded)
        {
            return;
        }
        Souls -= soulsNeeded;
        vacuumGun.LevelUpScale(VaccumScaleLevel);
        VaccumScaleLevel += 1;

    }

    public void UnlockWallVacuum()
    {
        int soulsNeeded = 30;
        if(Souls < soulsNeeded)
        {
            return;
        }
        Souls -= soulsNeeded;
        unlockedVacuums.Add(Vacuum.Wall);
    }

    public void UnlockChargeVacuum()
    {
        int soulsNeeded = 30;
        if(Souls < soulsNeeded)
        {
            return;
        }
        Souls -= soulsNeeded;
        unlockedVacuums.Add(Vacuum.Charge);
    }





  }
