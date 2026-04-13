using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

    private List<string> perksList = new List<string>
    {
        "GlassCannon", "Thorns", "Ninja", "Brute"
    };
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
        if(perksList.Count == 0)
        {
            Debug.Log("No perks left!");
            return;
        }

        int randomIndex = Random.Range(0, perksList.Count);
        string selectedPerk = perksList[randomIndex];

        perksList.RemoveAt(randomIndex);

        Debug.Log("Selected perk: " + selectedPerk);
    }
}
