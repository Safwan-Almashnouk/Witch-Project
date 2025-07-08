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
          
            timer = 0;
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = ObjectPool.SharedInstance.SpawnFromPool(PoolTag.EnemyBullet, "Beatle", transform.position, transform.rotation);

        if (bullet != null)
        {
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Vector3 targetPos = player.transform.position;
                bullet.GetComponent<EnemyBulletScript>().Initialize(targetPos);
            }

            bullet.SetActive(true);
        }
    }


}
