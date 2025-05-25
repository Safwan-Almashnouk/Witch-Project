using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeContext : MonoBehaviour
{
    private IMeleeStrategy _meleeStrategy;


    private MovementManager _movementManager;


    public void SetAttackStrategy(IMeleeStrategy Newstrategy)
    {
        _meleeStrategy = Newstrategy;
    }

    public void AttackDone()
    {
        if (_meleeStrategy != null)
        {
            StartCoroutine(_meleeStrategy.ExecuteAttack());
        }
    }

    public void DealDamage()
    {
        if (_meleeStrategy != null)
        {
            _meleeStrategy.DealDamage();
        }
    }

    public void EndAttack()
    {
        if (_meleeStrategy != null)
        {
            {
                _meleeStrategy.EndAttack();
            }
        }
    }
}
