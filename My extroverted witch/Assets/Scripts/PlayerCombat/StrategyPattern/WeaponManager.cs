using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private Context context;
    private FireBall fire;
    private IceBall ice;
    
    void Start()
    {
        context = GetComponent<Context>();
        fire = GetComponent<FireBall>();
        context.SetAttackStrategy(fire);
        ice = GetComponent<IceBall>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
             context.AttackDone();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            context.SetAttackStrategy(ice);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            context.SetAttackStrategy(fire);
        }

    }
}
