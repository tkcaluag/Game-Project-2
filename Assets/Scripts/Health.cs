using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] private int health = 100;
    private int maxHealth = 100;

    // Update is called once per frame
    void Update()
    {
        
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

    }

    private IEnumerator VisualIndicator(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.15f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void Die()
    {
        Debug.Log("DEAD");
        Destroy(gameObject);
    }

    public void SetHealth(int maxHealth, int health)
    {
        this.maxHealth = maxHealth;
        this.health = health;
    }
}
