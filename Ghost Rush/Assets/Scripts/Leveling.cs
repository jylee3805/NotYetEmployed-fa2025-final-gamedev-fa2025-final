using UnityEngine;

public class Leveling : MonoBehaviour
{
    public VacuumGun vacuumGun;
    public int Souls { get; private set; }
    public static Leveling Instance { get; private set; }

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

  }
