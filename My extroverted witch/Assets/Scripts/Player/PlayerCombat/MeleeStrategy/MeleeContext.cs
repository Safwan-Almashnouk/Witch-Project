using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeContext : MonoBehaviour
{
    private IMeleeStrategy _meleeStrategy;


    public void SetAttackStrategy(IMeleeStrategy Newstrategy)
    {
        _meleeStrategy = Newstrategy;
    }
}
