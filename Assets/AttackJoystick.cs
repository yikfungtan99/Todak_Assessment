using UnityEngine;

public class AttackJoystick : Joystick
{
    protected override void Tap()
    {
        base.Tap();
        
    }

    protected override void Hold()
    {
        base.Hold();
        //PlayerManager.Instance.CurrentUnit.UnitAttack.StraightAim(Quaternion.Euler(currentDirection));
    }

    protected override void EndHold()
    {
        base.EndHold();
        //PlayerManager.Instance.CurrentUnit.UnitAttack.EndAim();
    }
}