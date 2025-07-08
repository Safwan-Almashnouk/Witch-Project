using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parrying : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            EnemyBulletScript bullet = other.GetComponent<EnemyBulletScript>();
            if (bullet != null)
            {
                bullet.tag = "PlayerProjectile";
                bullet.Reflect();
                gameObject.SetActive(false);
            }
        }
    }

}
