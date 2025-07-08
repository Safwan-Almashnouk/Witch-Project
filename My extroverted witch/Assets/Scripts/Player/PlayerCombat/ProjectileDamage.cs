using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float energyGiven;
  
    void Start()
    {
        Physics2D.IgnoreLayerCollision(3, 7);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")&& gameObject.tag == "PlayerProjectile")
        {
            Physics2D.IgnoreLayerCollision(3, 7);
            Health targetHealth = collision.gameObject.GetComponent<Health>();
            targetHealth.TakeDamage(damage, gameObject);
            EnergyManager.Instance.AddEnergy(energyGiven);
        }

        if(collision.gameObject.CompareTag("Player")&& gameObject.tag == "EnemyProjectile")
        {
            Health targetHealth = collision.gameObject.GetComponent<Health>();
            targetHealth.TakeDamage(damage, gameObject);
            EnergyManager.Instance.AddEnergy(energyGiven);
        }
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }


}
