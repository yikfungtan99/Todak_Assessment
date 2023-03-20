using UnityEngine;

[CreateAssetMenu(fileName = "Sphere Ability", menuName = "TODAK_ASSESSMENT/ABILITY/SPHERE ABILITY")] 
public class SphereSO : AbilitySO
{
    public SphereAbility sphereAbility;
    public override Ability InitAbility(Unit unit)
    {
        return new SphereAbility(sphereAbility, unit);
    }
}