using UnityEngine;

public class AbilityJoyStick : Joystick
{
    [SerializeField] private int abilityIndex;
    [SerializeField] private bool canAim = true;
    [SerializeField] private bool usePointAim = false;

    protected override void Tap()
    {
        base.Tap();
        PlayerManager.Instance.CurrentUnit.UnitAttack.SetAbility(abilityIndex);
        PlayerManager.Instance.CurrentUnit.UnitAttack.Perform();
    }

    protected override void Hold()
    {
        if (!canAim) return;
        base.Hold();
        PlayerManager.Instance.CurrentUnit.UnitAttack.SetAbility(abilityIndex);

        if (usePointAim)
        {
            Debug.Log(stickDistance);
            PlayerManager.Instance.CurrentUnit.UnitAttack.PointAim(Quaternion.Euler(currentDirection));
        }
        else
        {
            PlayerManager.Instance.CurrentUnit.UnitAttack.StraightAim(Quaternion.Euler(currentDirection));
        }
    }

    protected override void EndHold()
    {
        base.EndHold();
        PlayerManager.Instance.CurrentUnit.UnitAttack.Perform(Quaternion.Euler(currentDirection));
        PlayerManager.Instance.CurrentUnit.UnitAttack.EndAim();
    }
}