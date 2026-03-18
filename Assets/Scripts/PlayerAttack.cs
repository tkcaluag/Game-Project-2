using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject attackArea;
    private bool attacking = false;
    private float timeToAttack = 0.25f;
    private float timer = 0;
    private Vector2 moveInput;
    public AudioSource src;

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
            timer  += Time.deltaTime;
            if(timer >= timeToAttack)
            {
                timer = 0;
                attacking = false;
                animator.SetBool("isAttacking", false);
                attackArea.SetActive(attacking);
            }
        }
    }

    public void Attack()
    {
        if(Time.timeScale != 0){
            attacking = true;
            animator.SetBool("isAttacking", true);
        

            attackArea.SetActive(attacking);
        }
    }
}
