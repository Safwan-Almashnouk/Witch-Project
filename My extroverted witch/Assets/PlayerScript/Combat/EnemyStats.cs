using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float CurrentHealth;
    private float MaxHealth;


    void Start()
    {
        MaxHealth = CurrentHealth;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            float damage = other.gameObject.GetComponent<ProjectileScript>().damage;
            CurrentHealth = CurrentHealth - damage;
            Destroy(other.gameObject);
            Debug.Log(CurrentHealth);
        }

    }
    void Update()
    {
        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
     void damage()
    {

    }
    
   
}
