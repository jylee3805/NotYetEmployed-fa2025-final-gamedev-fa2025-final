using UnityEngine;

public class Leveling : MonoBehaviour
{
    public int level = 1;
    public float currentXp = 0f;
    public float xpToNextLevel = 100f;
    public VacuumGun vacuumGun;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddXp(float amount)
    {
        currentXp += amount;

        if (currentXp >= xpToNextLevel)
        {
            currentXp -= xpToNextLevel;
            xpToNextLevel *= 1.15f;
            LevelUp();
        }
    }

    void LevelUp()
    {
        level++;
        vacuumGun.LevelUp(level);
    }
}
