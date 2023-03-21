using UnityEngine;

[CreateAssetMenu(fileName = "Normal Attack Ability", menuName = "TODAK_ASSESSMENT/ABILITY/NORMAL ATTACK")]
public class NormalAttackAbilitySO : AbilitySO
{
    public NormalAttackAbility normalAttackAbility;
    public override Ability InitAbility(Unit unit)
    {
        return new NormalAttackAbility(normalAttackAbility, unit);
    }
}