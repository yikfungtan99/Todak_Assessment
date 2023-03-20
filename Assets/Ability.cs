using UnityEngine;

[System.Serializable]
public abstract class Ability
{
    public bool CanAim = false;
    public float Range;
    public AimType AimType;

    protected Unit unit;
    protected Quaternion currentAim;

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

    public abstract void Perform(Quaternion rotation);
    public abstract void UpdateAbility();
    public virtual void Hold(Quaternion dir)
    {
        currentAim = dir;
        switch (AimType)
        {
            case AimType.STRAIGHT:
                AimManager.Instance.StraightAim(dir);
                break;

            case AimType.POSITIONAL:
                AimManager.Instance.PointAim(dir);
                break;

            default:
                break;
        }
        
    }

    public virtual void EndHold()
    {
        Perform(currentAim);
        AimManager.Instance.EndAim();
    }
}