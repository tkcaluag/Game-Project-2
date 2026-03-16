using UnityEngine;

public class AttackArea : MonoBehaviour
{

    [SerializeField] private int damage = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.GetComponent<Health>() != null)
        {
            Health health = collider.GetComponentInParent<Health>();
            health.Damage(damage);
        }
    }
}
