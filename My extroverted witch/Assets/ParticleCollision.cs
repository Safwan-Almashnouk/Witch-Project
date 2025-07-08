using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    private ParticleSystem particles;
    [SerializeField] private float dps;
    [SerializeField] private float damageInterval = 1f;

    private Dictionary<GameObject, float> damageTimers = new Dictionary<GameObject, float>();

    void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        
        List<GameObject> toRemove = new List<GameObject>();
        foreach (var entry in damageTimers)
        {
            if (entry.Key == null)
            {
                toRemove.Add(entry.Key);
            }
        }
        foreach (var enemy in toRemove)
        {
            damageTimers.Remove(enemy);
        }

        if (!other.CompareTag("Enemy")) return;

        float lastDamageTime;
        damageTimers.TryGetValue(other, out lastDamageTime);

        if (Time.time - lastDamageTime >= damageInterval)
        {
            damageTimers[other] = Time.time;

           
            Health health = other.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(dps, gameObject);
                EnergyManager.Instance.AddEnergy(15);
            }
        }
    }
}
