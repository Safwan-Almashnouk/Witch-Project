using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class FireBallStrategy : MonoBehaviour, IFightStrategy
{

    [Header("Bullet stats")]
    [SerializeField] float bulletSpeed;
    [SerializeField]float Interval;
    [SerializeField]float timer;
    [SerializeField] GameObject bullet;

    [Header("Bullet Parameters")]
    [SerializeField] internal Vector2 lookDirection;
    [SerializeField] internal float lookAngle;
    [SerializeField] internal Transform firePoint;
    [SerializeField]internal bool canfire = true;

    [Header("SpecailAbilities")]
    [SerializeField] private float cooldownTime, nextReadyTime;
    [SerializeField] ParticleSystem flames;
    private bool CanUseAbility;
    private bool isFiring;

    [Header("Others")]
    private PlayerMovement playerMovement;
    private WeaponManager weaponManager;
    private Context context;
    private FireballUltimate ultimateMove;
    private FireCharged charge;

    [Header("UI")]
    [SerializeField] Image Special;

    void Start()
    {
        weaponManager = GetComponent<WeaponManager>();
        playerMovement = GetComponentInParent<PlayerMovement>();
        ultimateMove = GetComponentInChildren<FireballUltimate>();
        charge = GetComponentInChildren<FireCharged>();
        Special.fillAmount = 1f;
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
        flames.transform.rotation = Quaternion.Euler(0, 0, lookAngle);

        float cooldownRemaining = nextReadyTime - Time.time;

        if (cooldownRemaining > 0)
        {
            Special.fillAmount = 1 - (cooldownRemaining / cooldownTime);
        }
        else
        {
            Special.fillAmount = 1f;  
        }

        if (flames.isEmitting == false)
        {
            isFiring = false;
            Debug.Log("Flames Stopped");
        }


        if (isFiring)
        {
            
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
        if (Time.time >= nextReadyTime)
        {
          nextReadyTime = Time.time + cooldownTime;
            
          isFiring = true;
          flames.Play();
        }
    }

  

    public void UseUltimate()
    {
        ultimateMove.Ultimate();
    }

    public void StartCharging()
    {
        charge.StartCharging();
    }

    public void FinishedCharging()
    {
        charge.ReleaseCharging();
    }
}
