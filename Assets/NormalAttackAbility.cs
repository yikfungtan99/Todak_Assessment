using System;
using UnityEngine;

[System.Serializable]
public class NormalAttackAbility : Ability
{
    public GameObject normalAttackPrefab;

    public NormalAttackAbility(Unit unit) : base(unit)
    {
    }

    public NormalAttackAbility(NormalAttackAbility ability, Unit unit) : base(ability, unit)
    {
        normalAttackPrefab = ability.normalAttackPrefab;
    }

    public override void Perform(Quaternion rotation)
    {
        Attack(rotation);
    }

    public override void UpdateAbility()
    {
        
    }

    public void Attack(Quaternion rotation)
    {
        Projectile projInstance = GameObject.Instantiate(normalAttackPrefab, unit.transform.position, rotation).GetComponent<Projectile>();
        projInstance.SetOwner(unit);
    }
}
