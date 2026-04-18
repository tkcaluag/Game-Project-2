using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] private int health = 100;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text currentHealth;
    [SerializeField] private float regenThreshold = 0.35f;
    [SerializeField] private float regenRate = 5f;
    [SerializeField] private float regenDelay = 2f;
    [SerializeField] private GameObject gameOver;
    private int maxHealth = 100;
    private bool thornsPerk = false;
    private Coroutine regenCoroutine;

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

        if((float)health / maxHealth <= regenThreshold)
        {
            if(regenCoroutine != null)
            {
                StopCoroutine(regenCoroutine);
            }

            regenCoroutine = StartCoroutine(RegenerateHealth());
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

    private IEnumerator RegenerateHealth()
    {
        yield return new WaitForSeconds(regenDelay);

        while (health < maxHealth)
        {
            health += (int)regenRate;

            if(health > maxHealth)
            {
                health = maxHealth;
            }

            UpdateUI();
            yield return new WaitForSeconds(1f);
        }
    }

    private void Die()
    {
        gameOver.SetActive(true);
        Destroy(gameObject);

        PlayerPrefs.DeleteAll();
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
        maxHealth += 25;
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

    public void GlassHealth()
    {
        maxHealth = maxHealth / 2;
        health = health / 2;
        SaveHealth();
        UpdateUI();
    }

    public bool Thorns()
    {
        thornsPerk = true;
        return thornsPerk;
    }

    public bool hasThorns()
    {
        return thornsPerk;
    }


}
