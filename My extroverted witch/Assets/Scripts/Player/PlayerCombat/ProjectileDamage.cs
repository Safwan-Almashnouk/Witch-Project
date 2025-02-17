using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    [SerializeField] float damage;


    void Start()
    {
        Physics2D.IgnoreLayerCollision(3, 7);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Health targetHealth = collision.gameObject.GetComponent<Health>();
            targetHealth.TakeDamage(damage);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


}
