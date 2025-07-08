using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
public class MeleeManager : MonoBehaviour
{
    private MeleeContext contexts;
    private GreatSwordStrategy greatSword;

    private MovementManager _movementManager;


    void Start()
    {
        contexts = GetComponentInChildren<MeleeContext>();
        greatSword = GetComponentInChildren<GreatSwordStrategy>();
        contexts.SetAttackStrategy(greatSword);
        _movementManager = GetComponentInChildren<MovementManager>();

       
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void WeaponDamage()
    {
        contexts.DealDamage();
    }

    public void EndAttack()
    {
        contexts.EndAttack();
    }

    public void Slash(InputAction.CallbackContext context)
    {
        if (context.performed &&_movementManager.CanAttack == true)
        {
            float value = context.ReadValue<float>();
            if (value > 0)  // Means button pressed down
            {
               _movementManager.SetAllMovementPermissions(false);
                contexts.AttackDone();
            }
     
        }

        
    }

    public void Parry(InputAction.CallbackContext context)
    {
       
        if (context.performed && _movementManager.CanAttack == true)
        {
            float value = context.ReadValue<float>();
            if (value > 0)  // Means button pressed down
            {
                _movementManager.SetAllMovementPermissions(false);
                contexts.Parry();
            }

        }
    }

    public void EndParry()
    {
        contexts.EndParry();
    }


}