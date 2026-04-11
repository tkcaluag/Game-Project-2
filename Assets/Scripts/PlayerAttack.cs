using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject attackArea;
    [SerializeField] GameObject buffedAttackArea;
    private bool attacking = false;
    private bool hasAttackAreaBuff = false;
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
        
            if(!hasAttackAreaBuff){
                attackArea.SetActive(attacking);
            } else
            {
                buffedAttackArea.SetActive(attacking);
            }
        }
    }

    public void getAttackAreaBuff()
    {
        hasAttackAreaBuff = true;
    }
}
