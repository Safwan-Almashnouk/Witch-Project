using System.Collections.Generic;
using UnityEngine;

public class FireSphere : MonoBehaviour
{
    private ParticleSystem particles;
    [SerializeField] private float dps;
    [SerializeField] private float damageInterval = 1f;
    [SerializeField] private float knockback;

    private Dictionary<GameObject, float> damageTimers = new Dictionary<GameObject, float>();

    private CircleCollider2D sphereCollider;
    private float sizeMultiplier = 1f;

    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        sphereCollider = GetComponent<CircleCollider2D>();
    }

    void LateUpdate()
    {
        sphereCollider.enabled = true;
        ParticleSystem.Particle[] particleArray = new ParticleSystem.Particle[particles.main.maxParticles];
        int aliveCount = particles.GetParticles(particleArray);
            
        if (aliveCount > 0)
        {
            float currentParticleSize = particleArray[0].GetCurrentSize(particles);
            sphereCollider.radius = (currentParticleSize / 2f) * sizeMultiplier;
            if(currentParticleSize >= 29.99f)
            {
                sphereCollider.enabled = false;
                Destroy(gameObject);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy")) return;

        Debug.Log($"Collision with {collision.gameObject.name} at {Time.time}");

        float lastDamageTime;
        damageTimers.TryGetValue(collision.gameObject, out lastDamageTime);

        if (Time.time - lastDamageTime >= damageInterval)
        {
            damageTimers[collision.gameObject] = Time.time;

           
            Health health = collision.gameObject.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(dps, gameObject);
                EnergyManager.Instance.AddEnergy(15);
            }

           
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null && rb.bodyType == RigidbodyType2D.Dynamic)
            {
                Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;

              
                rb.velocity = Vector2.zero;

                rb.AddForce(knockbackDirection * knockback, ForceMode2D.Impulse);
                Debug.Log($"Applied knockback to {collision.gameObject.name}");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy")) return;

        float lastDamageTime;
        damageTimers.TryGetValue(collision.gameObject, out lastDamageTime);

        if (Time.time - lastDamageTime >= damageInterval)
        {
            damageTimers[collision.gameObject] = Time.time;

            Health health = collision.gameObject.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(dps, gameObject);
                EnergyManager.Instance.AddEnergy(15);
            }

            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null && rb.bodyType == RigidbodyType2D.Dynamic)
            {
                Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
                rb.velocity = Vector2.zero;
                rb.AddForce(knockbackDirection * knockback, ForceMode2D.Impulse);
                Debug.Log($"Applied knockback to {collision.gameObject.name}");
            }
        }
    }
}


