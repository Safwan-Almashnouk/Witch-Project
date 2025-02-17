using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthManager : MonoBehaviour
{

    [SerializeField] Slider healthSlider;
    [SerializeField] Health health;
    void Start()
    {
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = health.CurrentHealth;
        Debug.Log(health.CurrentHealth);
    }
}
