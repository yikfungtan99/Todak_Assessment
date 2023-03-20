using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimManager : SingletonBehaviour<AimManager>
{
    [Header("Dependencies")]
    [SerializeField] private CanvasGroup straightRangeIndicator;
    [SerializeField] private CanvasGroup pointRangeIndicator;
    [SerializeField] private CanvasGroup rangeRadius;

    [Header("Properties")]
    [SerializeField] private float showTime;

    private float curShowTime;
    private float curFadeTime;

    private float currentPointRangeAimSpeed;

    private bool isAiming;
    private bool isStraightAim = false;
    private bool isPointAim = false;

    private Quaternion straightAimDirection;

    private Vector3 pointAimPosition;

    // Update is called once per frame
    void Update()
    {
        if (isAiming)
        {
            curFadeTime = 0;
            curShowTime += Time.deltaTime;

            float t = Mathf.Clamp01(curShowTime / showTime);

            float alpha = Mathf.Lerp(0f, 1f, t);
            rangeRadius.alpha = alpha;

            if (isStraightAim)
            {
                straightRangeIndicator.alpha = alpha;
                Quaternion newRotation = Quaternion.Euler(90f, straightAimDirection.eulerAngles.y, 0);
                straightRangeIndicator.transform.rotation = newRotation;
            }

            if (isPointAim)
            {
                pointRangeIndicator.alpha = alpha;
                pointRangeIndicator.transform.position = pointAimPosition;
            }
        }
        else
        {
            straightRangeIndicator.alpha = 0;
            pointRangeIndicator.alpha = 0;
            rangeRadius.alpha = 0;
        }
    }

    public void StraightAim(Quaternion aim)
    {
        isAiming = true;
        isStraightAim = true;
        straightAimDirection = aim;
    }

    public void PointAim(Vector3 scaledPosition)
    {
        isAiming = true;
        isPointAim = true;
        pointAimPosition = new Vector3(scaledPosition.x, pointRangeIndicator.transform.position.y, scaledPosition.y);
    }

    public void EndAim()
    {
        isAiming = false;
        isStraightAim = false;
        isPointAim = false;
        pointRangeIndicator.transform.position = new Vector3(transform.position.x, pointRangeIndicator.transform.position.y, transform.position.z);
    }

}
