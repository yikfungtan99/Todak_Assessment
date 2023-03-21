using System;
using UnityEngine;
using UnityEngine.UI;

public class AbilityJoyStick : Joystick
{
    [SerializeField] private Image abilityBackground;

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
        if (!ability.CanAim)
        {
            ability.Perform();
            return;
        }

        base.Hold();

        if (abilityBackground != null) abilityBackground.gameObject.SetActive(true);
        stickImage.gameObject.SetActive(true);

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

        if (abilityBackground != null) abilityBackground.gameObject.SetActive(false);
        stickImage.gameObject.SetActive(false);

        Vector3 localTouchPos = cancelZone.InverseTransformPoint(touchPosition);

        if (localTouchPos.magnitude < cancelZone.sizeDelta.x)
        {
            ability.Cancel();
        }

        ability.EndHold();
    }
}