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

    public GameObject projectileAimPlayer;
    public GameObject projectileAimMinions;

    Transform target;

    public float wallRange;

    [SerializeField] float stopDist = 5;
    [SerializeField] float backOffDist = 9;

    public GameObject[] wallArray;
    public Transform[] moveSpots;
    public GameObject[] allies;
    public GameObject[] minions;
    int randomSpot;
    public float startWaitTime;
    float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        allies = GameObject.FindGameObjectsWithTag("Enemy");
        minions = GameObject.FindGameObjectsWithTag("Ally");
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
                foreach (GameObject minion in minions)
                {
                    if(minion != null)
                    {
                        if (Vector2.Distance(transform.position, target.position) > backOffDist && Vector2.Distance(transform.position, minion.transform.position) > backOffDist - 2)
                        {

                            Patrol();
                            AvoidAllies();
                        }
                        else
                        {

                            float distanceToTarget = (transform.position - minion.transform.position).sqrMagnitude;
                            Debug.Log(distanceToTarget);
                            if (distanceToTarget < backOffDist + 1 && minion.activeSelf == true && minion.transform != null)
                            {

                                ChaseMinion(minion.transform);
                                AvoidAllies();
                            }
                            else
                            {
                                ChasePlayer();
                                AvoidAllies();
                            }


                        }
                    }
                    else
                    {
                        if (Vector2.Distance(transform.position, target.position) > backOffDist)
                        {

                            Patrol();
                            AvoidAllies();
                        }
                        else
                        {
                            ChasePlayer();
                            AvoidAllies();
                        }

                    }
                    
                }
                
            }
                
        }
    }

    private void ChaseMinion(Transform tr)
    {
        Debug.Log("aadasd");
        
            if (Vector2.Distance(transform.position, tr.position) > stopDist)
            {
                transform.position = Vector2.MoveTowards(transform.position, tr.position, speed * Time.deltaTime);
            }
            if (Vector2.Distance(transform.position, tr.position) < stopDist - 2)
            {
                transform.position = Vector2.MoveTowards(transform.position, tr.position, retreatSpeed * Time.deltaTime);
            }
        ShootMinions();
    }

    void AvoidAllies()
    {
        foreach (GameObject tr in allies)
        {
            float distanceSqr = (transform.position - tr.transform.position).sqrMagnitude;
            if (distanceSqr < stopDist - 0.5)
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
            Instantiate(projectileAimPlayer, transform.position, Quaternion.identity);
            timeBtwnShots = startTimeBtwnShots;
        }
        else
        {
            timeBtwnShots -= Time.deltaTime;
        }
    }

    void ShootMinions()
    {
        if (timeBtwnShots <= 0)
        {
            Instantiate(projectileAimMinions, transform.position, Quaternion.identity);
            timeBtwnShots = startTimeBtwnShots;
        }
        else
        {
            timeBtwnShots -= Time.deltaTime;
        }
    }

}
