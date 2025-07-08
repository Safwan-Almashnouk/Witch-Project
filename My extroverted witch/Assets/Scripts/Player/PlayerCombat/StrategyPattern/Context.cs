using UnityEngine;

public class Context : MonoBehaviour
{
    private IFightStrategy _strategy;
    private Coroutine attackCoroutine;

    public void SetAttackStrategy(IFightStrategy newStrategy)
    {
        _strategy = newStrategy;
    }

    public void StartAttack()
    {
        if (_strategy != null && attackCoroutine == null)
        {
            attackCoroutine = StartCoroutine(_strategy.ExecuteAttack());
        }
    }

    public void StopAttack()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
    }

    public void UseSpecialAbility()
    {
        _strategy.UseSpecial();

    }

    public void UltimateArts()
    {
        _strategy.UseUltimate();
    }
 
}
