using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseUI;
    public bool isPaused = false;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI soulsText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI scaleText;
    public Button healthButton;
    public Button speedButton;
    public Button damageButton;
    public Button scaleButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthButton.onClick.AddListener(Leveling.Instance.UpgradeHealth);
        speedButton.onClick.AddListener (Leveling.Instance.UpgradeSpeed);
        damageButton.onClick.AddListener(Leveling.Instance.UpgradeDamage);
        scaleButton.onClick.AddListener(Leveling.Instance.UpgradeVaccumScale);
    }

    // Update is called once per frame
    void Update()
    {

        soulsText.text = "Souls: " + Leveling.Instance.Souls;
        healthText.text = "Health Level: " + Leveling.Instance.HealthLevel + "\nSoul Cost: " + (10 + Leveling.Instance.HealthLevel * 2);
        speedText.text = "Speed Level: " + Leveling.Instance.SpeedLevel + "\nSoul Cost: " + (10 + Leveling.Instance.SpeedLevel * 2);
        damageText.text = "Damage Level: " + Leveling.Instance.DamageLevel + "\nSoul Cost: " + (10 + Leveling.Instance.DamageLevel * 2);
        scaleText.text = "Scale Level: " + Leveling.Instance.VaccumScaleLevel + "\nSoul Cost: " + (Leveling.Instance.VaccumScaleLevel * 2);


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        } 
    }

    public void PauseGame()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        healthText.text = "Health Level: " + Leveling.Instance.HealthLevel;
        speedText.text = "Speed Level: " + Leveling.Instance.SpeedLevel;
    }

    public void ResumeGame() {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
