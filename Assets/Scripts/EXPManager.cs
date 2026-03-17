using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EXPManager : MonoBehaviour
{
    public int level;
    public int currentExp;
    public int expToLevel = 10;
    public float growthMultiplier = 1.2f;
    public Slider expSlider;
    public TMP_Text currentLevelText;

    private void Start()
    {
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
    }

    public void UpdateUI()
    {
        expSlider.maxValue = expToLevel;
        expSlider.value = currentExp;
        currentLevelText.text = "Level: " + level; 
    }

}
