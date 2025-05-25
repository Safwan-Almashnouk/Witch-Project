using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [SerializeField] internal bool CanJump, CanMove, CanDash, CanAttack = true;
    void Start()
    {
        CanDash = true;
        CanAttack = true;
        CanJump = true;
        CanMove = true;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void SetAllMovementPermissions(bool value)
    {
        CanJump = value;
        CanDash = value;
        CanMove = value;
    }
}
