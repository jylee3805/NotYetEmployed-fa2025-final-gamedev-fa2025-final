using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VacuumCarousel
{
    public Sprite image { get; set; }
    public Vacuum vacuum { get; set; }
}

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
    public Button vaccumLeft;
    public Button vaccumRight;
    public Button equip;
    public TextMeshProUGUI equipText;
    public TextMeshProUGUI vacuumText;
    public AudioSource generalAudio;
    public AudioSource sfxAudio;

    public Slider musicSlider;
    public Slider sfxSlider;
    public SpriteRenderer vacuumSprite;

    public List<Sprite> vacuums;
    public Image innerImage;
    public int carouselIndex = 0;
    public List<Vacuum> vacuumTypes;

    private string getVacuumName(Vacuum type)
    {
        if (type == Vacuum.Default)
        {
            return "Default";
        }
        else if (type == Vacuum.Charge)
        {
            return "Charged";
        }
        else if (type == Vacuum.Wall)
        {
            return "Wall";
        }
        else return null;


    }
    private void moveRight()
    {
        carouselIndex++;
        if(carouselIndex > 2)
        {
            carouselIndex = 0;
        }
        innerImage.sprite = vacuums[carouselIndex];
        vacuumText.text = getVacuumName(vacuumTypes[carouselIndex]);
    }

    private void moveLeft()
    {
        carouselIndex--;
        if (carouselIndex < 0)
        {
            carouselIndex = 2;
        }
        innerImage.sprite = vacuums[carouselIndex];
        vacuumText.text = getVacuumName(vacuumTypes[carouselIndex]);
    }

    private void setVacuum()
    {
        if (!Leveling.Instance.unlockedVacuums.Contains(vacuumTypes[carouselIndex]))
        {

            switch (vacuumTypes[carouselIndex])
            {
                case Vacuum.Charge:
                    Leveling.Instance.UnlockChargeVacuum();
                    break;
                case Vacuum.Wall:
                    Leveling.Instance.UnlockWallVacuum();
                    break;
            }

            if (!Leveling.Instance.unlockedVacuums.Contains(vacuumTypes[carouselIndex]))
            {
                return;
            }
                
        }
        VacuumGun.Instance.currentVac = vacuumTypes[carouselIndex];
        vacuumSprite.sprite = vacuums[carouselIndex]; 
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthButton.onClick.AddListener(Leveling.Instance.UpgradeHealth);
        speedButton.onClick.AddListener (Leveling.Instance.UpgradeSpeed);
        damageButton.onClick.AddListener(Leveling.Instance.UpgradeDamage);
        scaleButton.onClick.AddListener(Leveling.Instance.UpgradeVaccumScale);
        vaccumLeft.onClick.AddListener(moveLeft);
        vaccumRight.onClick.AddListener(moveRight);
        equip.onClick.AddListener(setVacuum);
        innerImage.sprite = vacuums[carouselIndex];
        vacuumText.text = getVacuumName(vacuumTypes[carouselIndex]);
        musicSlider.value = .25f;
        sfxSlider.value = .25f;
        SetVolumeSFX(.25f);
        SetVolume(.25f);

        musicSlider.onValueChanged.AddListener(SetVolume);
        sfxSlider.onValueChanged.AddListener(SetVolumeSFX);
        
    }

    // Update is called once per frame
    void Update()
    {

        soulsText.text = "Souls: " + Leveling.Instance.Souls;
        healthText.text = "Health Level: " + Leveling.Instance.HealthLevel + "\nSoul Cost: " + (10 + Leveling.Instance.HealthLevel * 2);
        speedText.text = "Speed Level: " + Leveling.Instance.SpeedLevel + "\nSoul Cost: " + (10 + Leveling.Instance.SpeedLevel * 2);
        damageText.text = "Damage Level: " + Leveling.Instance.DamageLevel + "\nSoul Cost: " + (10 + Leveling.Instance.DamageLevel * 2);
        scaleText.text = "Scale Level: " + Leveling.Instance.VaccumScaleLevel + "\nSoul Cost: " + (Leveling.Instance.VaccumScaleLevel * 2);
        equipText.text = Leveling.Instance.unlockedVacuums.Contains(vacuumTypes[carouselIndex]) ? "Equip" : "Buy";

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

    public void SetVolume(float value)
    {
        generalAudio.volume = value;  
    }

      public void SetVolumeSFX(float value)
    {
        sfxAudio.volume = value;  
    }

}
