using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject normalBulletPrefab;
    public GameObject healingBulletPrefab;
    public GameObject specialBulletPrefab;
    GameObject kappa;

    [SerializeField] float bulletType;
    public float bulletForce = 20f;

    private void Start()
    {
        bulletType = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
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
    }

    void Shoot()
    {
        if (bulletType == 1)
        {
            kappa = normalBulletPrefab;
        }else if(bulletType == 2)
        {
            kappa = healingBulletPrefab;
        }else if (bulletType == 3)
        {
            kappa = specialBulletPrefab;
        }

        GameObject bullet = Instantiate(kappa, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

        Destroy(bullet, 1);
    }
}
