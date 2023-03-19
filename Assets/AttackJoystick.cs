using UnityEngine;

public class AttackJoystick : Joystick
{
    protected override void Tap()
    {
        base.Tap();
        PlayerManager.Instance.CurrentUnit.UnitAttack.Attack();
    }

    protected override void Hold()
    {
        base.Hold(); 
        PlayerManager.Instance.CurrentUnit.UnitAttack.Aim();
    }
}