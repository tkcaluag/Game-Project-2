using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject attackArea;
    private bool attacking = false;
    private float timeToAttack = 0.25f;
    private float timer = 1;
    private Vector2 moveInput;

    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attacking)
        {
            Debug.Log("Attack Start");
            timer  += Time.deltaTime;
            if(timer >= timeToAttack)
            {
                timer = 0;
                attacking = false;
                animator.SetBool("isAttacking", false);
                attackArea.SetActive(attacking);
                Debug.Log("Attack End");
            }
        }
    }

    public void Attack()
    {
        attacking = true;
        animator.SetBool("isAttacking", true);

        attackArea.SetActive(attacking);

        

    }
}
