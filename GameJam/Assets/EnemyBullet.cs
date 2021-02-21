using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    public float speed;
   //public PlayerController playerController;
    //public PlayerController enemyController;
    public GameObject hitEffect;

    Transform player;
    Vector3 target;
    public GameObject minions;
    public int targetNo;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        minions = GameObject.FindGameObjectWithTag("Ally");

        if(targetNo == 1)
        {
            target = new Vector3(player.position.x, player.position.y, player.position.z);
            transform.right = target - transform.position;
        }
        if(targetNo == 2)
        {

            /*float distanceSqr = (transform.position - minions.transform.position).sqrMagnitude;*/

            target = new Vector3(minions.transform.position.x, minions.transform.position.y, minions.transform.position.z);
            //transform.right = target - transform.position;



        }
        
        

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        //transform.position += transform.forward * speed * Time.deltaTime;
        //transform.Translate(Vector3.right * speed * Time.deltaTime, Space.Self);

        Destroy(gameObject, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Player"){
            collision.gameObject.GetComponent<PlayerController>().Damage(10);

        }
        
        if (collision.tag == "Ally"){
            collision.gameObject.GetComponent<PlayerController>().Damage(10);
        }
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1);
        //Destroy(gameObject);

        if (collision.tag != "EnemyDetect")
        {
            Destroy(gameObject);
        }

    }
}
