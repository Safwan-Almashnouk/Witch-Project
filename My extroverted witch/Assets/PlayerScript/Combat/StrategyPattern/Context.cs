using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Context : MonoBehaviour
{
    private IFightStrategy _strategy;



    public void SetAttackStrategy(IFightStrategy Newstrategy)
    {
        _strategy = Newstrategy;
    }


   public void AttackDone()
    {
        if (_strategy != null)
        {

            StartCoroutine(_strategy.ExecuteAttack());
        }
    }
}
