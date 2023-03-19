using System;
using UnityEngine;

public class TeleportAbility : Ability
{
    private bool canTeleport = false;

    private float projectileDecayTime = 3f;

    private float currentTime = 0f;

    Projectile projInstance = null;

    public TeleportAbility(Unit unit) : base(unit)
    {
    }

    public override void UpdateAbility()
    {
        if (!canTeleport) return;
        currentTime += Time.deltaTime;

        if (currentTime >= projectileDecayTime)
        {
            canTeleport = false;
            currentTime = 0;
            projInstance = null;
        }
    }

    public override void Perform(Quaternion aim)
    {
        if (!canTeleport)
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
        canTeleport = false;
    }

    private void FireTeleportProjectile(Quaternion aim)
    {
        if (canTeleport) return;
        projInstance = GameObject.Instantiate(unit.UnitAttack.AttackProjectilePrefab, unit.transform.position, aim);
        projInstance.CanDestroyOtherUnits = false;
        projInstance.SetOwner(unit);
        projInstance.DecayTime = projectileDecayTime;
        canTeleport = true;
    }
}