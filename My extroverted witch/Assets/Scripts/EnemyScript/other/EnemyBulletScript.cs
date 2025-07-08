using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float force;


    private Vector3 direction;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Initialize(Vector3 targetPosition)
    {
        
        direction = (targetPosition - transform.position).normalized;
        rb.velocity = direction * force;
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
        gameObject.tag = "EnemyBullet";
    }

    public void Reflect()
    {
        rb.velocity = -rb.velocity;
        direction = -direction;
    }
}
