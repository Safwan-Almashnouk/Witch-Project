using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampCombat : MonoBehaviour
{
    public float RateOfFire;
    public float AttackSpeed;
    public GameObject bullet;
    public GameObject MeleeWeapon;
    public float bulletSpeed;
    Vector2 lookDirection;
    float lookAngle;
    public Transform firePoint;
    private bool canfire = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lookDirection = new Vector2(lookDirection.x - transform.position.x, lookDirection.y - transform.position.y);
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
       
        firePoint.rotation = Quaternion.Euler(0, 0, lookAngle);

        if (Input.GetMouseButtonDown(0))
        {
            if (canfire) 
            {
                StartCoroutine(Fire());
            }

            
        }
    }

    IEnumerator Fire()
    {
        canfire = false;
        GameObject bulletClone = Instantiate(bullet);
        bulletClone.transform.position = firePoint.position;
        bulletClone.transform.rotation = Quaternion.Euler(0, 0, lookAngle);
        bulletClone.GetComponent<Rigidbody2D>().velocity = firePoint.right * bulletSpeed;
        StartCoroutine(RoF());
        yield return null;
    }
    IEnumerator RoF()
    {
        
        yield return new WaitForSeconds(RateOfFire);
        canfire = true;
        
    }
}