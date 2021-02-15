using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Ally") != null)
        {
            GameObject ally = GameObject.FindGameObjectWithTag("Ally");
            Physics2D.IgnoreCollision(ally.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1);
        Destroy(gameObject);
        
    }
}
