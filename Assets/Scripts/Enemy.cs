using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private int damage = 5;
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private EnemyData data;
    private GameObject player;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        SetEnemeyValues();
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
            if(collider.GetComponent<Health>() != null)
            {
                collider.GetComponent<Health>().Damage(damage);
            }
        }
    }

    private void SetEnemeyValues()
    {
        GetComponent<Health>().SetHealth(data.health, data.health);
        damage = data.damage;
        speed = data.speed;
    }
}
