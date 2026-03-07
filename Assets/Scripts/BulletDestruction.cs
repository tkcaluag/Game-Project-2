using UnityEngine;

public class BulletDestruction : MonoBehaviour
{

    public float lifetime = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
