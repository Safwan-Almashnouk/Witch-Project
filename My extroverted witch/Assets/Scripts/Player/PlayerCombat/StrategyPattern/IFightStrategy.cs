using System.Collections;

public interface IFightStrategy
{
    IEnumerator ExecuteAttack();
    void UseSpecial();

    void UseUltimate();

    
}
