using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class CoinCounter : MonoBehaviour
{
    public static CoinCounter instance;
    
    public TMP_Text coinText;
    public GameObject winScreen;
    public int currentCoins = 0;

    void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coinText.text = "COINS: " + currentCoins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentCoins == 5)
        {
            Time.timeScale = 0;
            currentCoins = 0;
            winScreen.SetActive(true);
        }
    }

    public void IncreaseCoins(int v)
    {
        currentCoins += v;
        coinText.text = "COINS: " + currentCoins.ToString();
    }
}
