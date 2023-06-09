﻿using UnityEngine;

[System.Serializable]
public abstract class Ability
{
    public bool CanAim = false;
    public float Range;
    public AimType AimType;

    protected Unit unit;
    protected Quaternion currentAim;
    protected Vector3 currentAimPosition;
    protected bool isCanceled = false;

    protected Ability(Unit unit)
    {
        this.unit = unit;
    }

    protected Ability(Ability ability, Unit unit)
    {
        this.unit = unit;

        CanAim = ability.CanAim;
        Range = ability.Range;
        AimType = ability.AimType;
    }

    public virtual void Perform()
    {
        Perform(unit.transform.rotation);
    }

    public virtual void Perform(Quaternion rotation)
    {

    }

    public virtual void Perform(Vector3 position)
    {

    }

    public virtual void Cancel()
    {
        isCanceled = true;
    }

    public abstract void UpdateAbility();

    public virtual void Hold(Quaternion dir)
    {
        if (AimType != AimType.STRAIGHT) return;
        currentAim = dir;
        AimManager.Instance.StraightAim(dir, Range);
    }

    public virtual void Hold(Vector3 scaledPosition)
    {
        if (AimType != AimType.POSITIONAL) return;
        currentAimPosition = scaledPosition;
        AimManager.Instance.PointAim(scaledPosition, Range);
    }

    public virtual void EndHold()
    {
        AimManager.Instance.EndAim();

        if (isCanceled)
        {
            isCanceled = false;
            return;
        }

        switch (AimType)
        {
            case AimType.STRAIGHT:
                Perform(currentAim);
                break;

            case AimType.POSITIONAL:
                Perform(currentAimPosition);
                break;
            default:
                break;
        }
    }
}