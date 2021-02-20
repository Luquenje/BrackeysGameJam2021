using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyController : MonoBehaviour
{
    public float speed;
    /*public float patrolSpeed;*/
    public float retreatSpeed;
    float timeBtwnShots;
    public float startTimeBtwnShots = 0.5f;

    public GameObject projectile;

    Transform target;
    Transform playerTarget;

    public float wallRange;

    [SerializeField] float stopDist = 5;
    [SerializeField] float playerStopDist = 5;
    [SerializeField] float backOffDist = 9;

    public GameObject[] wallArray;
    public Transform[] moveSpots;
    public GameObject[] allies;
    public GameObject[] enemies;
    int randomSpot;
    public float startWaitTime;
    float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        //target = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();
        playerTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        allies = GameObject.FindGameObjectsWithTag("Ally");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
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
                transform.position = Vector2.MoveTowards(transform.position, tr.transform.position, -2 * Time.deltaTime);
            }
            else
            {
                /*if(enemies != null)
                {*/
                    foreach (GameObject help in enemies)
                    {
                        if (Vector2.Distance(transform.position, help.transform.position) > backOffDist)
                        {
                            FollowPlayer();
                            AvoidAllies();
                        }
                        else
                        {
                            ChaseEnemy();
                            AvoidAllies();
                        }


                    }
                //}
                /*else
                {
                    FollowPlayer();
                }*/
                

            }

        }
    }

    private void FollowPlayer()
    {
        if (Vector2.Distance(transform.position, playerTarget.position) > playerStopDist)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTarget.position, speed * Time.deltaTime);
        }
        if (Vector2.Distance(transform.position, playerTarget.position) < playerStopDist - 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTarget.position, retreatSpeed * Time.deltaTime);
        }
        /*else
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTarget.position, speed * Time.deltaTime);
        }*/
    }

    void AvoidAllies()
    {
        foreach (GameObject tr in allies)
        {
            float distanceSqr = (transform.position - tr.transform.position).sqrMagnitude;
            if (distanceSqr < playerStopDist - 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, tr.transform.position, -3 * Time.deltaTime);
                
            }
            /*else
            {
                if (Vector2.Distance(transform.position, target.position) > backOffDist)
                {
                    FollowPlayer();
                }
                else
                {
                    ChaseEnemy();
                }

            }*/

        }
    }

    void ChaseEnemy()
    {
        foreach (GameObject help in enemies)
        {
            if (Vector2.Distance(transform.position, help.transform.position) > stopDist)
            {
                transform.position = Vector2.MoveTowards(transform.position, help.transform.position, speed * Time.deltaTime);
            }
            if (Vector2.Distance(transform.position, help.transform.position) < stopDist - 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, help.transform.position, retreatSpeed * Time.deltaTime);
            }
        }
        Shoot();
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInactiveInRadius();
    }

    /*void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, patrolSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }*/

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
