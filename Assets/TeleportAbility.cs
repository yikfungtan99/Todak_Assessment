using System;
using UnityEngine;

[System.Serializable]
public class TeleportAbility : Ability
{
    public Projectile TeleportAbilityPrefab;
    public float ProjectileDecayTime = 3f;
    public float CurrentTime = 0f;

    Projectile projInstance = null;

    public TeleportAbility(Unit unit) : base(unit)
    {
    }

    public TeleportAbility(TeleportAbility ability, Unit unit) : base(ability, unit)
    {
    }

    public override void UpdateAbility()
    {
        
    }

    public override void Perform(Quaternion aim)
    {
        if (projInstance == null)
        {
            FireTeleportProjectile(aim);
        }
        else
        {
            Teleport();
        }

    }

    private void Teleport()
    {
        if (projInstance == null) return;
        
        unit.transform.position = projInstance.transform.position;
        GameObject.Destroy(projInstance.gameObject);
        projInstance = null;
    }

    private void FireTeleportProjectile(Quaternion aim)
    {
        projInstance = GameObject.Instantiate(unit.UnitAbility.AttackProjectilePrefab, unit.transform.position, aim);
        projInstance.CanDestroyOtherUnits = false;
        projInstance.SetOwner(unit);
        projInstance.DecayTime = ProjectileDecayTime;
    }
}