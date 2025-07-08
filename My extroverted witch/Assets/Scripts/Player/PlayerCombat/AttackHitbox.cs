using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    private float dmg;
    private GameObject attacker;
    [SerializeField] private LayerMask enemyLayers;

    private HashSet<GameObject> hitEnemies = new HashSet<GameObject>();

    public void Setup(float damage, GameObject source)
    {
        dmg = damage;
        attacker = source;
        hitEnemies.Clear(); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TryDamage(other);
    }

    public void ManualHit()
    {
        Collider2D[] results = new Collider2D[10]; 
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(enemyLayers);
        filter.useLayerMask = true;

        int count = Physics2D.OverlapCollider(GetComponent<Collider2D>(), filter, results);

        for (int i = 0; i < count; i++)
        {
            TryDamage(results[i]);
        }
    }


    private void TryDamage(Collider2D col)
    {
        if (hitEnemies.Contains(col.gameObject)) return;

        if (((1 << col.gameObject.layer) & enemyLayers) != 0)
        {
            Health health = col.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(dmg, attacker);
                hitEnemies.Add(col.gameObject);
                
                
               
            }
        }
    }
}
