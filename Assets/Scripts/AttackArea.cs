using Unity.VisualScripting;
using UnityEngine;

public class AttackArea : MonoBehaviour
{

    [SerializeField] private int damage = 3;
    public AudioSource src;
    [SerializeField] private AudioClip soundEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!PlayerPrefs.HasKey("Damage"))
        {
            PlayerPrefs.SetInt("Damage", damage);
        } else
        {
            LoadDamage();
            Debug.Log("Damage: " + damage);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.GetComponent<Enemy>() != null)
        {
            Enemy enemy_health = collider.GetComponentInParent<Enemy>();
            enemy_health.Damage(damage);

            src.clip = soundEffect;
            src.Play();

        }
    }

    public void LevelUpAttack()
    {
        damage += 25;
        SaveDamage();
    }

    public void SaveDamage()
    {
        PlayerPrefs.SetInt("Damage", damage);
    }

    public void LoadDamage()
    {
        damage = PlayerPrefs.GetInt("Damage");
    }
}
