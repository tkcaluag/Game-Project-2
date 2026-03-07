using UnityEngine;

public class Coins : MonoBehaviour
{
    public int value;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision!");
            Destroy(gameObject);
            CoinCounter.instance.IncreaseCoins(value);
        }
    }
}
