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
    private int maxHealth = 100;

    private void Start()
    {
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
        Destroy(gameObject);
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
        UpdateUI();
    }

    public void UpdateUI()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
        currentHealth.text = "HP: " + health + '/' + maxHealth; 
    }
}
