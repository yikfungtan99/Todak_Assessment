using UnityEngine;

[CreateAssetMenu(fileName = "Teleport Ability", menuName = "TODAK_ASSESSMENT/ABILITY/TELEPORT ABILITY")]
public class TeleportSO : AbilitySO
{
    public TeleportAbility teleportAbility;

    public override Ability InitAbility(Unit unit)
    {
        return new TeleportAbility(teleportAbility, unit);
    }
}
