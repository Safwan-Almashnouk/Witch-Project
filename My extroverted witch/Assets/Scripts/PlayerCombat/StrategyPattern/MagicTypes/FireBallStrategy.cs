using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallStrategy : MonoBehaviour, IFightStrategy
{

   
    public GameObject bullet;
    public float bulletSpeed;
    Vector2 lookDirection;
    float lookAngle;
    public Transform firePoint;
    public bool canfire = true;
    public float Interval;
    public float timer;

    private Context context;
    

    public IEnumerator ExecuteAttack()
    {
        timer += Time.deltaTime;

        if(timer >= Interval)
        {
            canfire = false;
            GameObject bulletClone = Instantiate(bullet);
            bulletClone.transform.position = firePoint.position;
            bulletClone.transform.rotation = Quaternion.Euler(0, 0, lookAngle);
            bulletClone.GetComponent<Rigidbody2D>().velocity = firePoint.right * bulletSpeed;
            StartCoroutine(RoF());
            yield return null;
            timer = 0f;
        }
        
        
    }   
    IEnumerator RoF()
    {

        yield return new WaitForSeconds(Interval);
        canfire = true;

    }
    void Update()
    {
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lookDirection = new Vector2(lookDirection.x - transform.position.x, lookDirection.y - transform.position.y);
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        firePoint.rotation = Quaternion.Euler(0, 0, lookAngle);

    }

}
