using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
public class GreatSwordStrategy : MonoBehaviour, IMeleeStrategy
{

    [Header("Attack Settings")]
    [SerializeField] GameObject attackArea; // The attack collider or empty object representing attack zone
    [SerializeField] float dmg;
    private Health enemyHP;
    private MeleeContext context;

    private Animator animator;

    [Header("Attack conditions")]
    [SerializeField] bool canAttack = true;
    private MovementManager _movementManager;
    [SerializeField] PolygonCollider2D coneCollider;


    [Header("Parrying")]
    [SerializeField] GameObject parryBox;


    void Start()
    {
        animator = GetComponentInParent<Animator>();
        _movementManager = GetComponentInParent<MovementManager>();

     
    }
    // Update is called once per frame
    void Update()
    {

    }


    public IEnumerator ExecuteAttack()
    {
        if (canAttack)
        {
            coneCollider.enabled = true;
            canAttack = false;
            animator.SetTrigger("IsAttacking");       
            yield return new WaitForSeconds(0.01f);
           
            canAttack = true;
           
        }
    }

    public void EndAttack()
    {
        _movementManager.SetAllMovementPermissions(true);
        canAttack = true;
        coneCollider.enabled = false;

    }

    public void DealDamage()
    {

        Collider2D hitbox = attackArea.GetComponent<Collider2D>();
        AttackHitbox hitboxScript = attackArea.GetComponent<AttackHitbox>();
        hitboxScript.Setup(dmg, gameObject);
        hitboxScript.ManualHit();
    }


    public void StartParry()
    {
        Collider2D parryWindow = parryBox.GetComponent<Collider2D>();
        animator.SetTrigger("IsParrying");
        parryWindow.enabled = true;

    }

    public void EndParry()
    {
        Collider2D parryWindow = parryBox.GetComponent<Collider2D>();
        parryWindow.enabled = false;
    }

}
