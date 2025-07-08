using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireBallStrategy : MonoBehaviour, IFightStrategy
{

    [Header("Bullet stats")]
    [SerializeField] float bulletSpeed;
    [SerializeField]float Interval;
    [SerializeField]float timer;
    [SerializeField] GameObject bullet;

    [Header("Bullet Parameters")]
    [SerializeField] Vector2 lookDirection;
    [SerializeField] float lookAngle;
    [SerializeField] Transform firePoint;
    [SerializeField]bool canfire = true;

    [Header("SpecailAbilities")]
    [SerializeField] private float maxEnegry;
    [SerializeField] private float currentEnergy;
    [SerializeField] private float energyRecharge;
    [SerializeField] ParticleSystem flames;
    private bool CanUseAbility;
    private bool isFiring;

    [Header("Others")]
    private PlayerMovement playerMovement;
    private WeaponManager weaponManager;
    private Context context;
    private FireballUltimate ultimateMove;

    [Header("UI")]
    [SerializeField] Slider energySlider;


    void Start()
    {
        weaponManager = GetComponent<WeaponManager>();
        playerMovement = GetComponentInParent<PlayerMovement>();
        ultimateMove = GetComponentInChildren<FireballUltimate>();
    }

    public IEnumerator ExecuteAttack()
    {
        while (true)
        {
            if (canfire)
            {
                GameObject bullet = ObjectPool.SharedInstance.SpawnFromPool(PoolTag.PlayerProjectile, "Fire", transform.position, transform.rotation);
                CanUseAbility = false;
                

                if (bullet != null)
                {
                    bullet.SetActive(true);
                    bullet.transform.position = firePoint.position;
                    bullet.transform.rotation = Quaternion.Euler(0, 0, lookAngle);
                    bullet.GetComponent<Rigidbody2D>().velocity = firePoint.right * bulletSpeed;
                    canfire = false;
                    StartCoroutine(RoF());
                }
            }

            yield return null; // wait one frame
        }
    }
    IEnumerator RoF()
    {
        yield return new WaitForSeconds(Interval);
        canfire = true;
        CanUseAbility = true;   
    }
    void Update()
    {
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lookDirection = new Vector2(lookDirection.x - transform.position.x, lookDirection.y - transform.position.y);
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0, 0, lookAngle);
        energySlider.value = currentEnergy;
        flames.transform.rotation = Quaternion.Euler(0, 0, lookAngle);

        if (currentEnergy <= 0)
        {
            StopSpecialAbility();
        }

        if (currentEnergy < maxEnegry)
        {
            RechargeEnergy();
        }

        if (isFiring)
        {
            currentEnergy -= 2 * Time.deltaTime;
            currentEnergy = Mathf.Max(0, currentEnergy);
            weaponManager.canSwitch = false;
            playerMovement.speed = 15;
        }
        else
        {
            weaponManager.canSwitch = true;
            playerMovement.speed = 30;
        }
    }
   

   public void UseSpecial()
    {
        if (CanUseAbility && currentEnergy >= maxEnegry)
        {
            isFiring = true;
            flames.Play();
        }
      
    }

    public void StopSpecialAbility()
    {
        isFiring = false;
        flames.Stop();
    }

    void RechargeEnergy()
    {
        if (!isFiring && currentEnergy < maxEnegry)
        {
            currentEnergy += energyRecharge * Time.deltaTime;
            currentEnergy = Mathf.Min(currentEnergy, maxEnegry);
         
        }
    }

    public void UseUltimate()
    {
        ultimateMove.Ultimate();
    }
}
