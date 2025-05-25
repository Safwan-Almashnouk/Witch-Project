using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMeleeStrategy
{
    IEnumerator ExecuteAttack();
    void DealDamage();

    void EndAttack();
}
