using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyBullet : MonoBehaviour
{
    public float speed;

    public GameObject hitEffect;

    Transform player;
    Vector3 target;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();


        target = new Vector3(player.position.x, player.position.y, player.position.z);
        transform.right = target - transform.position;


    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        //transform.position += transform.forward * speed * Time.deltaTime;
        transform.Translate(Vector3.right * speed * Time.deltaTime, Space.Self);

        Destroy(gameObject, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1);
        Destroy(gameObject);

    }
}
