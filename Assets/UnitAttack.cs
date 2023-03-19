using System;
using UnityEngine;

public class UnitAttack : UnitBehaviour
{
    [SerializeField] private Projectile attackProjectilePrefab;

    public void Attack()
    {
        Projectile projInstance = Instantiate(attackProjectilePrefab, transform.position, transform.rotation);
        projInstance.SetOwner(unit);
    }

    public void Aim()
    {
        Debug.Log("AIMING");
    }
}