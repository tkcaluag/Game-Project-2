using System.Xml.XPath;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 100;
    private int maxHealth = 100;
    [SerializeField] private int damage = 5;
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private EnemyData data;
    [SerializeField] private int ExperiencePoints = 0;
    [SerializeField] private float knockbackForce = 5f;
    [SerializeField] private float knockbackTime = 0.2f;
    public delegate void EnemyDefeated(int experience);
    public static event EnemyDefeated OnEnemyDefeated;
    private Rigidbody2D rb;
    private GameObject player;
    private Animator animator;
    private bool isKnockedBack = false;
    private bool isDead = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        SetEnemyValues();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            Swarm();
        }
    }

    private void Swarm()
    {
        if (isKnockedBack)
        {
            return;
        }

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
            if(collider.GetComponent<PlayerHealth>() != null && !isDead)
            {
                collider.GetComponent<PlayerHealth>().Damage(damage);

                if (collider.GetComponent<PlayerHealth>().hasThorns())
                {
                    Damage(damage/2);
                }
            }
        }
    }

    private IEnumerator VisualIndicator(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.15f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private IEnumerator Knockback()
    {
        isKnockedBack = true;

        Vector2 direction = (transform.position - player.transform.position).normalized;
        rb.linearVelocity = direction * knockbackForce;

        yield return new WaitForSeconds(knockbackTime);

        rb.linearVelocity = Vector2.zero;
        isKnockedBack = false;
    }

    public void Damage(int value)
    {
        if(value < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative damage");
        }

        this.health -= value;
        StartCoroutine(VisualIndicator(Color.red));
        StartCoroutine(Knockback());

        if(health <= 0 && !isDead)
        {
            isDead = true;
            animator.SetBool("isDead", true);
            StartCoroutine(Die());
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

    private IEnumerator Die()
    {
        OnEnemyDefeated(ExperiencePoints);
        player.GetComponent<PlayerHealth>().Heal(5);
        yield return new WaitForSeconds(0.75f);
        Destroy(gameObject);
    }
}
