using System.Xml.XPath;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using UnityEditor.Callbacks;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 100;
    private int maxHealth = 100;
    [SerializeField] private int damage = 5;
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private EnemyData data;
    [SerializeField] private int ExperiencePoints = 0;
    public delegate void EnemyDefeated(int experience);
    public static event EnemyDefeated OnEnemyDefeated;
    
    private GameObject player;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        SetEnemyValues();
    }

    // Update is called once per frame
    void Update()
    {
        Swarm();
    }

    private void Swarm()
    {
        if(player != null){
            animator.SetBool("isWalking", true);
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        } else
        {
            animator.SetBool("isWalking", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if(collider.GetComponent<PlayerHealth>() != null)
            {
                collider.GetComponent<PlayerHealth>().Damage(damage);
            }
        }
    }

    private IEnumerator VisualIndicator(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.15f);
        GetComponent<SpriteRenderer>().color = Color.white;
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

    public int GetExperiencePoints()
    {
        return ExperiencePoints;
    }

    private void SetEnemyValues()
    {
        health = data.health;
        maxHealth = data.maxHealth;
        damage = data.damage;
        speed = data.speed;
        ExperiencePoints = data.ExperiencePoints;
    }

    private void Die()
    {
        OnEnemyDefeated(ExperiencePoints);
        player.GetComponent<PlayerHealth>().Heal(25);
        Destroy(gameObject);
    }
}
