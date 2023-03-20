using UnityEngine;


public abstract class AbilitySO : ScriptableObject
{
    public abstract Ability InitAbility(Unit unit);
}