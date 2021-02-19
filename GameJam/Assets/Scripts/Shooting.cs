using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject player;
    public Transform firePoint;
    public GameObject normalBulletPrefab;
    public GameObject healingBulletPrefab;
    public GameObject specialBulletPrefab;
    public Rigidbody2D rB;
    GameObject kappa;
    Vector2 mousePos;
    public Camera cam;
    Vector2 xAxis;
    Vector2 characterPos;

    [SerializeField] float bulletType;
    public float bulletForce = 20f;

    private void Start()
    {
        bulletType = 1;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        //characterPos = cam.WorldToScreenPoint(gameObject.transform.position);
        if (Input.GetButtonDown("Fire1"))
        {
            //characterPos = cam.WorldToScreenPoint(gameObject.transform.position);
            //xAxis = new Vector2(0, 1);
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            bulletType = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            bulletType = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            bulletType = 3;
        }

        //mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        //characterPos = cam.WorldToScreenPoint(gameObject.transform.position);
        //mousePos = Input.mousePosition;
        //xAxis = new Vector2(0, 1); //Represents direction of X-axis
    }

    private void FixedUpdate()
    {
        Vector2 lookDir = mousePos - rB.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.identity;
        rotation.eulerAngles = new Vector3(0, 0, angle); //Assuming it's top down, with only y rotations
        firePoint.transform.rotation = rotation;

    }

    void Shoot()
    {
        if (bulletType == 1)
        {
            kappa = normalBulletPrefab;
        }
        else if (bulletType == 2)
        {
            kappa = healingBulletPrefab;
        }
        else if (bulletType == 3)
        {
            kappa = specialBulletPrefab;
        }

        //Vector2 lookDir = mousePos - rB.position;
        //float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        //rB.rotation = angle;

        //Vector2 characterPos = cam.WorldToScreenPoint(player.transform.position);

        //Vector2 xAxis = new Vector2(0, 1); //Represents direction of X-axis

        //Vector2 newLine = new Vector2(mousePos.x - characterPos.x, mousePos.y - characterPos.y);
        //float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        //Quaternion rotation = Quaternion.identity;
        //rotation.eulerAngles = new Vector3(0, angle, 0); //Assuming it's top down, with only y rotations

        //GameObject bullet = (GameObject)Instantiate(kappa, firePoint.position, Quaternion.identity);
        //Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        //rb.AddForce(newLine * bulletForce * Time.deltaTime, ForceMode2D.Impulse);

        //Destroy(bullet, 1);


        //Vector2 newLine = new Vector2(mousePos.x - characterPos.x, mousePos.y - characterPos.y);
        //float angle = Vector2.Angle(xAxis, newLine);
        //Quaternion rotation = Quaternion.identity;
        //rotation.eulerAngles = new Vector3(0, angle, 0); //Assuming it's top down, with only y rotations

        GameObject bullet = Instantiate(kappa, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

        Destroy(bullet, 1);
    }
    
}
