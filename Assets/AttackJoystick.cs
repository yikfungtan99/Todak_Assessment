using UnityEngine;

public class AttackJoystick : Joystick
{
    protected override void Tap()
    {
        base.Tap();
        PlayerManager.Instance.CurrentUnit.UnitAttack.Perform();
    }

    protected override void Hold()
    {
        base.Hold();
        PlayerManager.Instance.CurrentUnit.UnitAttack.StraightAim(Quaternion.Euler(currentDirection));
    }

    protected override void EndHold()
    {
        base.EndHold();
        PlayerManager.Instance.CurrentUnit.UnitAttack.Perform(Quaternion.Euler(currentDirection));
        PlayerManager.Instance.CurrentUnit.UnitAttack.EndAim();
    }
}