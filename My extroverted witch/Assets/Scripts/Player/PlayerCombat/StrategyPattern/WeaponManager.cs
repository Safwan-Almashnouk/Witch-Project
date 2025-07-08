using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private Context context;
    private FireBallStrategy fire;
    private IceBallStrategy ice;
    internal bool canSwitch = true;

    void Start()
    {
        context = GetComponent<Context>();
        fire = GetComponent<FireBallStrategy>();
        ice = GetComponent<IceBallStrategy>();
        context.SetAttackStrategy(fire);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            context.StartAttack();
        }

        if (Input.GetMouseButtonUp(0))
        {
            context.StopAttack();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (canSwitch) 
            {
                context.SetAttackStrategy(fire);
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (canSwitch)
            {
                context.SetAttackStrategy(ice);
            }
        }
    }

    void Eability()
    {
        context.UseSpecialAbility();
    }

    void Ultimate() 
    { 
        context.UltimateArts();
    }
}
