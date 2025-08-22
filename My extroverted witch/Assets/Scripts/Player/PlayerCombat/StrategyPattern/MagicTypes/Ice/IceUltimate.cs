using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using UnityEngine.UI;

public class IceUltimate : MonoBehaviour
{

    [Header("Functions")]
    public float radius;
    public float freezeDuration;
    [SerializeField] private float cooldownTime, nextReadyTime;
    public LayerMask freezeMask;

    [Header("Other Scripts")]
    private MovementManager movementManager;

    [Header("UI")]
    public Image UltimateCoolDown;
    void Start()
    {
        movementManager = GetComponentInParent<MovementManager>();
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
    public void Ultimate(Vector2 center)
    {
      
        if (Time.time >= nextReadyTime)
        {

            if (EnergyManager.Instance.TryUseEnergy(300))
            {
                
                Collider2D[] targets = Physics2D.OverlapCircleAll(center, radius, freezeMask);
                foreach (Collider2D target in targets)
                {
                   
                    Debug.Log(target.gameObject.name);
                    TimeFreeze freezable = target.GetComponent<TimeFreeze>();
                    if (freezable != null)
                    {
                        freezable.Freeze(freezeDuration);
                        
                    }
                    
                }
                nextReadyTime = Time.time + cooldownTime;

            }
        
    }

}
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }


}
