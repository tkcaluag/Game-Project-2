using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] private int health = 100;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text currentHealth;
    [SerializeField] private GameObject gameOver;
    private int maxHealth = 100;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("MaxHealth") && !PlayerPrefs.HasKey("CurrentHealth"))
        {
            PlayerPrefs.SetInt("MaxHealth", 100);
            PlayerPrefs.SetInt("CurrentHealth", 100);
        }
        
        LoadHealth();
        UpdateUI();
    }
    public void Damage(int value)
    {
        if(value < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative damage");
        }

        this.health -= value;
        StartCoroutine(VisualIndicator(Color.red));

        if(health <= 0)
        {
            Die();
        }

        UpdateUI();
    }

    public void Heal(int value)
    {
        if(value < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative heals");
        }

        if(health + value > maxHealth)
        {
            this.health = maxHealth;
        } else

        {
           this.health += value; 
        }

        UpdateUI();

    }

    private IEnumerator VisualIndicator(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.15f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void Die()
    {
        gameOver.SetActive(true);
        Destroy(gameObject);

        PlayerPrefs.DeleteKey("MaxHealth");
        PlayerPrefs.DeleteKey("CurrentHealth");
        PlayerPrefs.DeleteKey("Level");
        PlayerPrefs.DeleteKey("ExperiencePoints");
        PlayerPrefs.DeleteKey("expToLevel");
        PlayerPrefs.DeleteKey("Damage");
    }

    public bool isDead()
    {
        if(health <= 0)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public void SetHealth(int maxHealth, int health)
    {
        this.maxHealth = maxHealth;
        this.health = health;

        UpdateUI();
    }

    public void LevelUpHealth()
    {
        maxHealth += 10;
        SaveHealth();
        UpdateUI();
    }

    public void UpdateUI()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
        currentHealth.text = "HP: " + health + '/' + maxHealth; 
    }

    public void SaveHealth()
    {
        PlayerPrefs.SetInt("MaxHealth", maxHealth);
        PlayerPrefs.SetInt("CurrentHealth", health);
    }

    public void LoadHealth()
    {
        maxHealth = PlayerPrefs.GetInt("MaxHealth");
        health = PlayerPrefs.GetInt("CurrentHealth");
    }
}
