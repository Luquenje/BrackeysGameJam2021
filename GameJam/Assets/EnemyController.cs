using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public float patrolSpeed;
    public float retreatSpeed;
    float timeBtwnShots;
    public float startTimeBtwnShots = 0.5f;

    public GameObject projectile;

    Transform target;

    public float wallRange;

    [SerializeField] float stopDist = 5;
    [SerializeField] float backOffDist = 9;

    public GameObject[] wallArray;
    public Transform[] moveSpots;
    int randomSpot;
    public float startWaitTime;
    float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        timeBtwnShots = startTimeBtwnShots;
        wallArray = GameObject.FindGameObjectsWithTag("Obstacle");
        randomSpot = Random.Range(0, moveSpots.Length);
    }

    void GetInactiveInRadius()
    {
        foreach (GameObject tr in wallArray)
        {
            float distanceSqr = (transform.position - tr.transform.position).sqrMagnitude;
            if (distanceSqr < wallRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, tr.transform.position, -3 * Time.deltaTime);
            }
            else
            {
                if (Vector2.Distance(transform.position, target.position) > backOffDist)
                {
                    Patrol();
                }
                else
                {
                    ChasePlayer();
                }

            }
                
        }
    }

    void ChasePlayer()
    {
        Shoot();
        if (Vector2.Distance(transform.position, target.position) > stopDist)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        if (Vector2.Distance(transform.position, target.position) < stopDist - 2)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, retreatSpeed * Time.deltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetInactiveInRadius();
    }

    void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, patrolSpeed * Time.deltaTime);
        if(Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
        {
            if(waitTime <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    void Shoot()
    {
        if (timeBtwnShots <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwnShots = startTimeBtwnShots;
        }
        else
        {
            timeBtwnShots -= Time.deltaTime;
        }
    }

}
