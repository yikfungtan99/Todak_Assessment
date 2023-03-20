using UnityEngine;

public class AbilityJoyStick : Joystick
{
    private Ability ability;

    public void SetAbility(Ability ability)
    {
        this.ability = ability;
    }

    protected override void Tap()
    {
        base.Tap();
        ability.Perform();
    }

    protected override void Hold()
    {
        if (!ability.CanAim) return;
        base.Hold();

        switch (ability.AimType)
        {
            case AimType.STRAIGHT:
                ability.Hold(Quaternion.Euler(currentDirection));
                break;

            case AimType.POSITIONAL:

                Vector3 playerPos = PlayerManager.Instance.CurrentUnit.transform.position;
                Vector3 actualPos = new Vector3(playerPos.x, playerPos.z);
                ability.Hold(GetScaledPosition(actualPos, ability.Range));
                break;

            default:
                break;
        }
    }

    protected override void EndHold()
    {
        base.EndHold();
        ability.EndHold();
    }
}