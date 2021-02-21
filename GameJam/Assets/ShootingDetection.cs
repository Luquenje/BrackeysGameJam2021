using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingDetection : MonoBehaviour
{
    public GameObject projectileAimPlayer;
    public GameObject projectileAimMinions;

    float timeBtwnShots;
    public float startTimeBtwnShots = 0.5f;

    public GameObject hitEffect;

    public Transform bulletSpawn;

    public GameObject[] minions;

    public Transform player;
    public float range = 9;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        minions = GameObject.FindGameObjectsWithTag("Ally");
        timeBtwnShots = startTimeBtwnShots;
        
    }

    private void Update()
    {
        minions = GameObject.FindGameObjectsWithTag("Ally");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        foreach (GameObject minion in minions)
        {
            if (minion != null)
            {
                if (Vector3.Distance(minion.transform.position, minion.transform.position) <= range && Vector3.Distance(minion.transform.position, minion.transform.position) <= Vector3.Distance(player.position, transform.position))
                {
                    ShootAlly();
                    Debug.Log(Vector2.Distance(player.position, transform.position));
                }
                else if(Vector3.Distance(player.position, transform.position) <= range)
                {
                    Shoot();
                    
                }
            }
        }

        if (Vector3.Distance(player.position, transform.position) <= range && minions == null)
        {
            Shoot();
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Shoot();

        }

        if (collision.tag == "Ally")
        {
            ShootAlly();
        }
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1);
        



    }*/


    void Shoot()
    {
        if (timeBtwnShots <= 0)
        {
            Instantiate(projectileAimPlayer, bulletSpawn.position, Quaternion.identity);
            timeBtwnShots = startTimeBtwnShots;
        }
        else
        {
            timeBtwnShots -= Time.deltaTime;
        }
    }
    void ShootAlly()
    {
        if (timeBtwnShots <= 0)
        {
            Instantiate(projectileAimMinions, bulletSpawn.position, Quaternion.identity);
            timeBtwnShots = startTimeBtwnShots;
        }
        else
        {
            timeBtwnShots -= Time.deltaTime;
        }
    }
}
