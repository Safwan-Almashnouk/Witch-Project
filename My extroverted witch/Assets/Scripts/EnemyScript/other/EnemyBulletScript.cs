using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
   [SerializeField] GameObject player;
   [SerializeField] Rigidbody2D rb;
   [SerializeField]float force;
    Vector3 dir;
    [SerializeField] internal float damage;
  
    

    // Start is called before the first frame update
    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        dir = (player.transform.position - transform.position).normalized;
        rb.velocity = dir * force;
        Physics2D.IgnoreLayerCollision(6, 7);


    }


        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health targetHealth = collision.gameObject.GetComponent<Health>();
            targetHealth.TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }

    

    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
