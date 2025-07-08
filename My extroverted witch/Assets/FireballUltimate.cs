using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FireballUltimate : MonoBehaviour
{
    [Header("Ultimate Energy")]
    [SerializeField] internal float maxEnergy;
    [SerializeField] internal float CurrentEnergy;
    [SerializeField] internal float damage;


    [Header("Functions")]
    [SerializeField] internal GameObject flameSphere;
    internal ParticleSystem particles;
    [SerializeField] private float cooldownTime, nextReadyTime;

    [Header("Other Scripts")]
    private MovementManager movementManager;

    private void Start()
    {
        movementManager = GetComponentInParent<MovementManager>();
        particles = flameSphere.GetComponent<ParticleSystem>();

    }
    public void Ultimate()
    {
        if (Time.time >= nextReadyTime) 
        {
            
            if (EnergyManager.Instance.TryUseEnergy(100))
            {
                Instantiate(flameSphere, transform.position, transform.rotation);
                particles.Play();
                nextReadyTime = Time.time + cooldownTime;

            }
        }
        if (particles.isPlaying == false)
        {
            movementManager.SetAllMovementPermissions(true);
            Destroy(flameSphere);
        }
    }
}

