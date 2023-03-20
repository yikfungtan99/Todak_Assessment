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
        TeleportAbilityPrefab = ability.TeleportAbilityPrefab;
    }

    public override void UpdateAbility()
    {
        
    }

    public override void Perform()
    {
        if (projInstance == null) return;
        Teleport();
    }

    public override void Perform(Quaternion aim)
    {
        if (projInstance != null)
        {
            Teleport();
        }
        else
        {
            FireTeleportProjectile(aim);
        }
    }

    public override void Hold(Quaternion dir)
    {
        if (projInstance != null) return;
        base.Hold(dir);
    }

    private void Teleport()
    {
        unit.transform.position = projInstance.transform.position;
        GameObject.Destroy(projInstance.gameObject);
        projInstance = null;
    }

    private void FireTeleportProjectile(Quaternion aim)
    {
        projInstance = GameObject.Instantiate(TeleportAbilityPrefab, unit.transform.position, aim);
        projInstance.SetOwner(unit);
    }
}