using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;
    public float bulletType;

    private void Start()
    {
        //type 2 is healing bullet
        if(bulletType != 2)
        {
            if (GameObject.FindGameObjectWithTag("Ally"))
            {
                GameObject ally = GameObject.FindGameObjectWithTag("Ally");
                Physics2D.IgnoreCollision(ally.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
            if (GameObject.FindGameObjectWithTag("Player"))
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1);
        Destroy(gameObject);
        
    }
}
