using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject attackArea;
    private bool attacking = false;
    private float timeToAttack = 0.25f;
    private float timer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
                attackArea.SetActive(attacking);
                Debug.Log("Attack End");
            }
        }
    }

    public void Attack()
    {
        attacking = true;
        attackArea.SetActive(attacking);

    }
}
