using UnityEngine;

public class Bars : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Transform healthFill;
    public Transform chargeFill;

    public PlayerMovement pmScript;

    public VacuumGun vgScript;
    private float healthScale;
    private float chargeScale;
    void Start()
    {
        healthScale = healthFill.localScale.x;
        chargeScale = chargeFill.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        float healthPct = pmScript.health / pmScript.maxHealth;
        float chargePct = vgScript.currentCharge / vgScript.maxCharge;

        healthFill.localScale = new Vector3(
            healthScale * healthPct,
            healthFill.localScale.y,
            1
        );

        healthFill.localPosition = new Vector3(
            -(healthScale - healthScale * healthPct) / 2f,
            healthFill.localPosition.y,
            healthFill.localPosition.z
        );

        chargeFill.localScale = new Vector3(
            chargeScale * chargePct,
            chargeFill.localScale.y,
            1
        );

        chargeFill.localPosition = new Vector3(
            -(chargeScale - chargeScale * chargePct) / 2f,
            chargeFill.localPosition.y,
            chargeFill.localPosition.z
        );
    }
}
