using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AllyAI : MonoBehaviour
{
    public GameObject target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    float timeBtwnShots;
    public float startTimeBtwnShots = 0.5f;

    public GameObject hitEffect;

    public Transform bulletSpawn;

    /*public GameObject[] minions;*/

    /*public Transform player;*/
    public float range = 9;

    public GameObject projectile;

    Seeker seeker;
    Rigidbody2D rb;

    Vector3 startingPos;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Enemy");

        InvokeRepeating("UpdatePath", 0f, 0.5f);

        startingPos = transform.position;

    }

    private void Update()
    {
        target = GameObject.FindGameObjectWithTag("Enemy");
        /*foreach (GameObject tr in target)
        {
            if (tr != null)
            {*/
        if (Vector3.Distance(target.transform.position, target.transform.position) <= range)
        {
            Shoot();
        }
        /*}*/
        /*}*/
    }

    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
            
                seeker.StartPath(rb.position, target.transform.position, OnPathComplete);
            
        }
    }


    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        //Pathfinding
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
        }
        else
        {
            reachedEndOfPath = false;
        }


        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }


        
    }

    void Shoot()
    {
        if (timeBtwnShots <= 0)
        {
            Instantiate(projectile, bulletSpawn.position, Quaternion.identity);
            timeBtwnShots = startTimeBtwnShots;
        }
        else
        {
            timeBtwnShots -= Time.deltaTime;
        }
    }
}
