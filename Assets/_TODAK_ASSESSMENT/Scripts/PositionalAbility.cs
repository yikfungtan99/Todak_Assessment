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
        positionalAbilityPrefab = ability.positionalAbilityPrefab;
    }

    public override void Perform()
    {
        
    }

    public override void Perform(Vector3 position)
    {
        Vector3 actualPos = new Vector3(position.x, unit.transform.position.y, position.y);

        GameObject.Instantiate(positionalAbilityPrefab, actualPos, Quaternion.identity);
    }

    public override void UpdateAbility()
    {

    }
}
