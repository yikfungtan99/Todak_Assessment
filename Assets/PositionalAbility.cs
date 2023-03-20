using UnityEngine;

[System.Serializable]
public class PositionalAbility : Ability
{
    public GameObject positionalAbilityPrefab;
    public PositionalAbility(Unit unit) : base(unit)
    {
    }

    public PositionalAbility(PositionalAbility ability, Unit unit) : base(ability, unit)
    {
    }

    public override void Perform(Quaternion aim)
    {

    }

    public override void UpdateAbility()
    {

    }
}
