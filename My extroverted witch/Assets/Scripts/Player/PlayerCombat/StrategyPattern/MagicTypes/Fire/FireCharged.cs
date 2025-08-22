using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FireCharged : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float holdStartTime;
    

    [Header("KeyFunctions")]
    [SerializeField] private InputAction chargeAttackAction;
    [SerializeField] private bool IsHolding;
    [SerializeField] private FireBallStrategy fireStrat;
    [SerializeField] private WeaponManager weaponManager;

    [Header("Visual")]
    [SerializeField] private GameObject fireball;
    [SerializeField] private GameObject MediumFireBall;
    [SerializeField] private GameObject LargeFireBall;




    private void Start()
    {
        fireStrat = GetComponentInParent<FireBallStrategy>();
        
        weaponManager = GetComponentInParent<WeaponManager>();
    }
    public void StartCharging()
    {
        holdStartTime = Time.time;
        IsHolding = true;
        weaponManager.canSwitch = false;
        fireStrat.canfire = false;
    }

    public void ReleaseCharging()
    {
        if (!IsHolding)
        {
            return;
        }
        float heldDuration = Time.time - holdStartTime;
        IsHolding = false;
        fireStrat.canfire = true;
        weaponManager.canSwitch = true;
        PerformChargedAttack(heldDuration);
    }


    private void PerformChargedAttack(float heldTime)
    {
        

        string selectedPrefab;
        float speed;
    

        if (heldTime < 1f)
        {
            
            selectedPrefab = "Sfire";
            speed = 70f;
          
        }
     
        else if (heldTime < 2f)
        {
            selectedPrefab = "Mfire";
            speed = 40f;
            
        }
        else
        {
            selectedPrefab = "Lfire";
            speed = 20f;
          
        }
       

        

        GameObject bullet = ObjectPool.SharedInstance.SpawnFromPool(PoolTag.PlayerProjectile,selectedPrefab , fireStrat.firePoint.position, Quaternion.Euler(0, 0, fireStrat.lookAngle));
        bullet.GetComponent<Rigidbody2D>().velocity = fireStrat.firePoint.right * speed;
    }

}
