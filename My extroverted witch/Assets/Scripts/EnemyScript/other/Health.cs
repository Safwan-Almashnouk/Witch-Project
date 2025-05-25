using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] internal float CurrentHealth;
    [SerializeField] float MaxHealth;


    void Start()
    {
        MaxHealth = CurrentHealth;
    }

    public void TakeDamage(float damage, GameObject attacker)
    {
        CurrentHealth -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage from {attacker.name}");
    }
    void Update()
    {

        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }

        Debug.Log(CurrentHealth);
    }
}
