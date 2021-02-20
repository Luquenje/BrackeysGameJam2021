using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

    // Insert Damage or Heal checks here
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Damage(20);
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
