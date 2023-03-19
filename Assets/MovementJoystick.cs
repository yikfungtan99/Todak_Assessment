using System;
using UnityEngine;
using UnityEngine.UI;

public class MovementJoystick : Joystick
{
    [SerializeField] private Image headingsImage;
    [SerializeField] private Transform headingsPivot;

    protected override void Update()
    {
        base.Update();
        UpdateHeadings();

        stickImage.enabled = !isMoving;
        headingsImage.enabled = !isMoving;
    }

    private void UpdateHeadings()
    {
        if (!isInitiated) return;

        Vector3 curDir = headingsPivot.rotation.eulerAngles;
        headingsPivot.rotation = Quaternion.Euler(new Vector3(curDir.x, curDir.y, currentHeadings));
    }
}