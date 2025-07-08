using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour
{
    public static EnergyManager Instance { get; private set; }

    [Header("Shared Energy")]
    [SerializeField]internal float maxEnergy;
    [SerializeField] internal float currentEnergy;

    [Header("UI")]
    [SerializeField] Slider UltimateSlider;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); 
    }

    private void Start()
    {
        currentEnergy = maxEnergy;
    }

    private void Update()
    {
        UltimateSlider.value = currentEnergy;
    }

    public bool TryUseEnergy(float amount)
    {
        if (currentEnergy >= amount)
        {
            currentEnergy -= amount;
            return true;
        }
        return false;
    }

    public void AddEnergy(float amount)
    {
        currentEnergy += amount;
    }
}
