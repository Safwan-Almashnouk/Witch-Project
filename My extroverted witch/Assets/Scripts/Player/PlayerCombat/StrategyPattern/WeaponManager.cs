using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private Context context;
    private FireBallStrategy fire;
    private IceBallStrategy ice;
    
    
    void Start()
    {
        context = GetComponent<Context>();
        fire = GetComponent<FireBallStrategy>();
        ice = GetComponent<IceBallStrategy>();
        context.SetAttackStrategy(fire);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            context.AttackDone();
        }




        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            context.SetAttackStrategy(fire);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            context.SetAttackStrategy(ice);
        }

    }
}
