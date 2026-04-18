using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Chest : MonoBehaviour
{

    [SerializeField] private TMP_Text GetPerkText;
    [SerializeField] private TMP_Text Explanation;
    [SerializeField] private TMP_Text PerkUI;
    [SerializeField] private GameObject ChestObject;
    [SerializeField] private Vector2 hiddenPosition;
    private List<string> availablePerksList = new List<string>
    {
        "Thorns", "Ninja", "Brute"
    };

    private List<string> playerPerksList = new List<string>{};
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadPerks();
        ApplyPerks();
        UpdatePerkUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if(availablePerksList.Count == 0)
            {
                Debug.Log("No perks left!");
                return;
            }

            int randomIndex = UnityEngine.Random.Range(0, availablePerksList.Count);
            string selectedPerk = availablePerksList[randomIndex];
            playerPerksList.Add(selectedPerk);

            availablePerksList.RemoveAt(randomIndex);

            if(selectedPerk == "GlassCannon")
            {
                collider.GetComponent<PlayerHealth>().GlassHealth();
                collider.GetComponentInChildren<AttackArea>(true).GlassAttack();
            }

            else if (selectedPerk == "Thorns")
            {
                collider.GetComponent<PlayerHealth>().Thorns();
            }

            else if (selectedPerk == "Ninja")
            {
                collider.GetComponent<Movement>().ninjaPerk();
            }

            else if (selectedPerk == "Brute")
            {
                collider.GetComponentInChildren<AttackArea>(true).BruteAttack();
                collider.GetComponent<Movement>().brutePerk();
            }

            SavePerks();
            UpdatePerkUI();
            StartCoroutine(ShowPerkNotification(selectedPerk));
            SendChestOut();
            Debug.Log("Selected perk: " + selectedPerk);
            Debug.Log("Available Perks: " + availablePerksList);
            Debug.Log("Player Perks: " + playerPerksList);
        }
    }

    private void SavePerks()
    {
       string availablePerks = string.Join(",", availablePerksList);
       string playerPerks = string.Join(",", playerPerksList);

       PlayerPrefs.SetString("playerPerks", playerPerks);
       PlayerPrefs.SetString("availablePerks", availablePerks);
       PlayerPrefs.Save();
    }

    private void LoadPerks()
    {
        if (PlayerPrefs.HasKey("availablePerks"))
        {
            string availablePerksData = PlayerPrefs.GetString("availablePerks");

            availablePerksList = string.IsNullOrEmpty(availablePerksData)
                ? new List<string>()
                : new List<string>(availablePerksData.Split(','));
        }

        if (PlayerPrefs.HasKey("playerPerks"))
        {
            string playerPerksData = PlayerPrefs.GetString("playerPerks");

            playerPerksList = string.IsNullOrEmpty(playerPerksData)
            ? new List<string>()
            : new List<string>(playerPerksData.Split(','));
        }
    }

    private void ApplyPerks()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) return;

        foreach (string perk in playerPerksList)
        {
            switch (perk)
            {
                case "Thorns":
                player.GetComponent<PlayerHealth>().Thorns();
                break;

                case "Ninja":
                player.GetComponent<Movement>().ninjaPerk();
                break;

                case "Brute":
                player.GetComponent<Movement>().brutePerk();
                break;
            }
        }
    }

    private void UpdatePerkUI()
    {
        PerkUI.text = "Active Perks: " + string.Join(", ", playerPerksList);
    }

    private IEnumerator ShowPerkNotification(string perkName)
    {
        GetPerkText.text = "You obtained: " + perkName + "!";

        if(perkName == "Thorns")
        {
            Explanation.text = " Enemies are damaged when they damage you";
        }

        else if(perkName == "Ninja")
        {
            Explanation.text = "Dash cooldown is significantly decreased but no longer provides invulnerability";
        }

        else if(perkName == "Brute")
        {
            Explanation.text = "Incrased attack damage but half your movement speed";
        }

        else if(perkName == "GlassCannon")
        {
            Explanation.text = "Significant increase in attack damage but significant decrease in health";
        }

        GetPerkText.gameObject.SetActive(true);
        Explanation.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        GetPerkText.gameObject.SetActive(false);
        Explanation.gameObject.SetActive(false);
    }

    public void SendChestOut()
    {
        ChestObject.transform.position = hiddenPosition;
    }

}
