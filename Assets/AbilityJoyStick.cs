using System;
using UnityEngine;

public class AbilityJoyStick : Joystick
{
    private Ability ability;
    private RectTransform cancelZone;

    public void SetAbility(Ability ability)
    {
        this.ability = ability;
    }

    public void SetCancelZone(RectTransform cancelZone)
    {
        this.cancelZone = cancelZone;
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

        Debug.Log(cancelZone.sizeDelta.x);

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

        Vector3 localTouchPos = cancelZone.InverseTransformPoint(touchPosition);


        if (localTouchPos.magnitude < cancelZone.sizeDelta.x)
        {
            ability.Cancel();
        }

        ability.EndHold();
    }
}