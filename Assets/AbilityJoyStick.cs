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
        base.Hold();
        ability.Hold(Quaternion.Euler(currentDirection));
    }

    protected override void EndHold()
    {
        base.EndHold();
        ability.EndHold();
    }
}