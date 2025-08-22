using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAmmoUser
{
    int CurrentAmmo { get; set; }
    int MaxAmmo { get; }
    void AddAmmo(int amount);
}