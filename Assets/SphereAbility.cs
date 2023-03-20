using UnityEngine;

[System.Serializable]
public class SphereAbility : Ability
{
    public GameObject sphereAbilityPrefab;

    public SphereAbility(Unit unit) : base(unit)
    {
    }

    public SphereAbility(SphereAbility ability, Unit unit) : base(ability, unit)
    {
    }

    public override void Perform(Quaternion aim)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateAbility()
    {

    }
}