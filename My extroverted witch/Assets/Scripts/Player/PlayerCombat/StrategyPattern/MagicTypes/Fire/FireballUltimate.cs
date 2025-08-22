using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FireballUltimate : MonoBehaviour
{
      


    [Header("Functions")]
    [SerializeField] internal GameObject flameSphere;
    internal ParticleSystem particles;
    [SerializeField] private float cooldownTime, nextReadyTime;
    [SerializeField] internal float damage;

    [Header("Other Scripts")]
    private MovementManager movementManager;

    [Header("UI")]
    public Image UltimateCoolDown;

    private void Start()
    {
        movementManager = GetComponentInParent<MovementManager>();
        particles = flameSphere.GetComponent<ParticleSystem>();
        UltimateCoolDown.fillAmount = 1f;

    }
    private void Update()
    {
        // Calculate cooldown progress and update UI fill amount
        float cooldownRemaining = nextReadyTime - Time.time;

        if (cooldownRemaining > 0)
        {
            UltimateCoolDown.fillAmount = 1 - (cooldownRemaining / cooldownTime);
        }
        else
        {
            UltimateCoolDown.fillAmount = 1f;  // Fully charged, ready to use
        }
    }
    public void Ultimate()
    {
        if (Time.time >= nextReadyTime) 
        {
            
            if (EnergyManager.Instance.TryUseEnergy(100))
            {
                 GameObject kaboom = Instantiate(flameSphere, transform.position, transform.rotation);
                kaboom.SetActive(true);
                particles.Play();
                nextReadyTime = Time.time + cooldownTime;

            }
        }
        if (particles.isPlaying == false)
        {
            movementManager.SetAllMovementPermissions(true);
        }
    }
}

