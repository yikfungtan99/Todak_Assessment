using UnityEngine;

public abstract class Ability
{
    protected Unit unit;

    protected Ability(Unit unit)
    {
        this.unit = unit;
    }

    public abstract void Perform(Quaternion rotation);
    public abstract void UpdateAbility();
}