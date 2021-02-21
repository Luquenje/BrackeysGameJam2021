using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    public GameObject allyPrefab;
    public GameObject gameover;
    public AudioClip enemyDie;
        public AudioSource playerHurt;
    public AudioClip hurt;

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
                playerHurt = GameObject.FindGameObjectWithTag("enemyDie").GetComponent<AudioSource>();
                
                playerHurt.PlayOneShot(enemyDie, 0.7f);
                Destroy(gameObject);
                Instantiate(allyPrefab, gameObject.transform.localPosition, Quaternion.identity);
            }

            if (gameObject.tag == "Player"){
                gameover.SetActive(true);
            }
            Destroy(gameObject);
        }
    }

    public void Damage(int damageAmount){
        currentHealth -= damageAmount;
        healthBar.SetHealth(currentHealth);
        if (gameObject.tag == "Player"){
                        playerHurt.PlayOneShot(hurt, 0.7f);
        }
    }

    public void Heal(int healAmount){
        currentHealth += healAmount;

        
    }
}
