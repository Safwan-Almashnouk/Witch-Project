using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] internal float CurrentHealth;
    [SerializeField] float MaxHealth;


    void Start()
    {
        MaxHealth = CurrentHealth;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
    }
    void Update()
    {

        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
