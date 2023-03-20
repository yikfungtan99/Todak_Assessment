using UnityEngine;

[CreateAssetMenu(fileName = "Normal Attack Ability", menuName = "TODAK_ASSESSMENT/ABILITY/POSITIONAL ABILITY")]
public class PositionalAbilitySO : AbilitySO
{
    public PositionalAbility positionalAbility;

    public override Ability InitAbility(Unit unit)
    {
        return new PositionalAbility(positionalAbility, unit);
    }
}