using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    public GameObject allyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        allyPrefab = Resources.Load("Ally 1") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0){
            if (gameObject.tag == "Enemy"){
                Instantiate(allyPrefab, gameObject.transform.localPosition, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }

    public void Damage(int damageAmount){
        currentHealth -= damageAmount;
        healthBar.SetHealth(currentHealth);
    }

    public void Heal(int healAmount){
        currentHealth += healAmount;

        
    }
}
