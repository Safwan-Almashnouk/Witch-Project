using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ShootingScript : MonoBehaviour
{
    public GameObject projectile;
    public float timer;
    public float Rof;
    public Transform projectilePos;
    private ChaseState CS;
    void Start()
    {
        CS = GetComponent<ChaseState>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
      

        if (timer > Rof & CS.canAttack == true)
        {
            Debug.Log(CS.canAttack);
            timer = 0;
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = EnemyObjectPooling.SharedInstance.GetPooledObject();
        if (bullet != null)
        {


            bullet.transform.position = gameObject.transform.position;
            bullet.transform.rotation = gameObject.transform.rotation;
            bullet.SetActive(true);
        }
    }
}
