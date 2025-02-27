using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMeleeStrategy
{
    int MaxCombo {get;} 
    void Attack(Animator animator, ComboSystem combo);
}
