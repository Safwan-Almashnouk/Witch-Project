using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBallStrategy : MonoBehaviour, IFightStrategy
{

    public float RateOfFire;
    public float AttackSpeed;
    public GameObject bullet;
    public float bulletSpeed;
    Vector2 lookDirection;
    float lookAngle;
    public Transform firePoint;
    public bool canfire = true;

    private Context context;
    

    public IEnumerator ExecuteAttack()
    {
        canfire = false;
        //GameObject bulletClone = Instantiate(bullet);
        GameObject bulletClone = IcePool.IceInstance.GetPooledObject();
        if (bulletClone != null)
        {
            bulletClone.transform.position = firePoint.position;
            bulletClone.transform.rotation = Quaternion.Euler(0, 0, lookAngle);
            bulletClone.SetActive(true);
            bulletClone.GetComponent<Rigidbody2D>().velocity = firePoint.right * bulletSpeed;
            StartCoroutine(RoF());
            yield return null;
        }

        
    }   
    IEnumerator RoF()
    {

        yield return new WaitForSeconds(RateOfFire);
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
