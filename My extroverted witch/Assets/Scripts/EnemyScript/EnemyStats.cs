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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            float damage = other.gameObject.GetComponent<ProjectileScript>().damage;
            CurrentHealth = CurrentHealth - damage;
            other.gameObject.SetActive(false);
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
