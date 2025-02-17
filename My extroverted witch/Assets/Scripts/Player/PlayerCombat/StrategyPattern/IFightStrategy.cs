using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFightStrategy
{
    IEnumerator ExecuteAttack();
    
}
