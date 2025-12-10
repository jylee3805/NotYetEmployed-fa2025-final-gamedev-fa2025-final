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
    public Button healthButton;
    public Button speedButton;
    public Button damageButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthButton.onClick.AddListener(Leveling.Instance.UpgradeHealth);
        speedButton.onClick.AddListener (Leveling.Instance.UpgradeSpeed);
        damageButton.onClick.AddListener(Leveling.Instance.UpgradeDamage);
    }

    // Update is called once per frame
    void Update()
    {

        soulsText.text = "Souls: " + Leveling.Instance.Souls;
        healthText.text = "Health Level: " + Leveling.Instance.HealthLevel + "\nNeeded Souls: " + Leveling.Instance.HealthLevel * 2;
        speedText.text = "Speed Level: " + Leveling.Instance.SpeedLevel + "\nNeeded Souls: " + Leveling.Instance.SpeedLevel * 2;
        damageText.text = "Damage Text: " + Leveling.Instance.DamageLevel + "\nNeeded Souls: " + Leveling.Instance.DamageLevel * 2;

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
