using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EXPManager : MonoBehaviour
{
    public int level;
    public int currentExp;
    public int expToLevel = 10;
    public float growthMultiplier = 1.2f;
    public GameObject Player;
    public Slider expSlider;
    public TMP_Text currentLevelText;
    public GameObject Attack;
    public GameObject UpgradeScreen;
    public GameObject Chest;

    private void Start()
    {

        if (!PlayerPrefs.HasKey("Level") && !PlayerPrefs.HasKey("ExperiencePoints") && !PlayerPrefs.HasKey("expToLevel"))
        {
            PlayerPrefs.SetInt("Level", 0);
            PlayerPrefs.SetInt("ExperiencePoints", 0);
            PlayerPrefs.SetInt("expToLevel", 10);
        } else
        {
            LoadLevel();
        }

        UpdateUI();
    }

    private void OnEnable()
    {
        Enemy.OnEnemyDefeated += gainExperience;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDefeated -= gainExperience;
    }

    public void gainExperience(int amount)
    {
        currentExp += amount;
        if(currentExp >= expToLevel)
        {
            LevelUp();
        }

        UpdateUI();
    }

    public void LevelUp()
    {
        level++;
        currentExp -= expToLevel;
        expToLevel = Mathf.RoundToInt(expToLevel * growthMultiplier);

        if(level%5 == 0)
        {
            Time.timeScale = 0;
            UpgradeScreen.SetActive(true);
        }

        if(level%10 == 0)
        {
            Chest.transform.position = Vector2.zero;
        }
        
        SaveLevel();
    }

    public void UpdateUI()
    {
        expSlider.maxValue = expToLevel;
        expSlider.value = currentExp;
        currentLevelText.text = "Level: " + level; 
    }

    public void SaveLevel()
    {
        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.SetInt("ExperiencePoints", currentExp);
        PlayerPrefs.SetInt("expToLevel", expToLevel);

        Debug.Log("Saved!");
    }

    public void LoadLevel()
    {
        level = PlayerPrefs.GetInt("Level");
        currentExp = PlayerPrefs.GetInt("ExperiencePoints");
        expToLevel = PlayerPrefs.GetInt("expToLevel");

        Debug.Log("Loaded!");
        Debug.Log("Level: " + level + " Current EXP: " + currentExp + " ExpToLevel: " + expToLevel);
    }

}
