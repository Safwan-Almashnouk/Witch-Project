using System.Collections.Generic;
using UnityEngine;
using System.Collections;
public class GreatSwordStrategy : MonoBehaviour, IMeleeStrategy
{

    [Header("Attack Settings")]
    [SerializeField] GameObject attackArea; // The attack collider or empty object representing attack zone
    [SerializeField] float dmg;
    [SerializeField] LayerMask enemyLayers; // Set this in inspector to enemy layer(s)
    [SerializeField] float attackRadius = 0.5f; // Radius for overlap check
    private Health enemyHP;
    private MeleeContext context;

    private Animator animator;

    [Header("Attack conditions")]
    [SerializeField] bool canAttack = true;
    [SerializeField] bool canMove = true;
    private MovementManager _movementManager;
    [SerializeField] PolygonCollider2D coneCollider;



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
    
    }

    public void DealDamage()
    {
        Debug.Log("hiii");
        Vector2 attackPos = attackArea.transform.position;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPos, attackRadius, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            // Try to get an enemy health component and deal damage
            Health enemyHealth = enemy.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(dmg, gameObject);
            }
        }
    }
}
