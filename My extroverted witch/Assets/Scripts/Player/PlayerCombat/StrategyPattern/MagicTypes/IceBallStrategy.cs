using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IceBallStrategy : MonoBehaviour, IFightStrategy
{
    [Header("Bullet stats")]
    [SerializeField] GameObject bullet;
    [SerializeField] float bulletSpeed;
    [SerializeField] Transform firePoint;
    [SerializeField] internal bool canfire = true;
    [SerializeField] float Interval;

    [Header("SpecailAbilities")]
    [SerializeField] internal float maxEnegry;
    [SerializeField] internal float currentEnergy;
    [SerializeField] internal float energyRecharge;
    private bool CanUseAbility;
    private bool isFiring;
    private IceBuilder iceSpecial;
    internal bool recharging = false;


    [Header("UI")]
    [SerializeField] Slider energySlider;

    [Header("Other")]
    Vector2 lookDirection;
    float lookAngle;
    private Context context;


    public void Start()
    {
        iceSpecial = GetComponentInChildren<IceBuilder>();
    }
    public IEnumerator ExecuteAttack()
    {
        while (true)
        {
            if (canfire)
            {
                GameObject bulletClone = ObjectPool.SharedInstance.SpawnFromPool(PoolTag.PlayerProjectile, "Ice", transform.position, transform.rotation);

                if (bulletClone != null)
                {
                    bulletClone.SetActive(true);
                    bulletClone.transform.position = firePoint.position;
                    bulletClone.transform.rotation = Quaternion.Euler(0, 0, lookAngle);
                    bulletClone.GetComponent<Rigidbody2D>().velocity = firePoint.right * bulletSpeed;

                    canfire = false;
                    StartCoroutine(RoF());
            
                }
            }
            else
            {
                yield return null;
            }
        }
    }
    IEnumerator RoF()
    {

        yield return new WaitForSeconds(Interval);
        canfire = true;

    }

    void Update()
    {
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lookDirection = new Vector2(lookDirection.x - transform.position.x, lookDirection.y - transform.position.y);
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0, 0, lookAngle);
        energySlider.value = currentEnergy;

        if (recharging == true) 
        { 
            RechargeEnergy();
            if(currentEnergy >= maxEnegry)
            {
                recharging = false;
            }
        }


    }
    public void UseSpecial()
    {
        if(currentEnergy >= maxEnegry)
        {
            iceSpecial.isActive = true;

        }
    }
    public void RechargeEnergy()
    {
         currentEnergy += energyRecharge * Time.deltaTime;
         currentEnergy = Mathf.Min(currentEnergy, maxEnegry);
    }
    public void UseUltimate()
    {

    }
}
