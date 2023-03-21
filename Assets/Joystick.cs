using System;
using UnityEngine;
using UnityEngine.UI;

public class Joystick : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] protected Image stickImage;
    [SerializeField] protected RectTransform stickRoot;

    [Header("Properties")]
    [SerializeField] protected float maxDistance = 50f;
    [SerializeField] protected float tapDuration = 0.1f;
    [SerializeField] protected float holdDuration = 0.2f;

    protected Vector3 defaultFingerPosition;

    private int cachedTouchIndex = -1;

    protected float currentHeadings = 0;

    protected Vector3 currentDirection;

    protected float currentTapTime = 0f;

    protected bool isInitiated;
    protected bool isTouching;
    protected bool isHolding = false;

    protected Vector2 touchPosition;
    public Vector3 Direction { get => currentDirection; }

    protected float stickDistance;
    public float StickDistance { get => stickDistance; }

    protected bool isMoving => !isTouching && !isInitiated;

    void Start()
    {
        maxDistance = stickRoot.sizeDelta.x / 2f - stickImage.rectTransform.sizeDelta.x / 2f;
        defaultFingerPosition = stickImage.rectTransform.position;
    }

    protected virtual void Update()
    {
        TouchInput();

        if (!isTouching)
        {
            stickImage.rectTransform.position = defaultFingerPosition;
        }
    }
    private Vector3 GetDirection(Vector2 pos)
    {
        if (!isTouching)
        {
            return Vector3.zero;
        }

        float angle = GetHeadings(pos);
        Vector3 direction = new Vector3(0, -angle, 0);

        return direction;
    }

    private float GetHeadings(Vector2 pos)
    {
        Vector2 direction = pos - (Vector2)defaultFingerPosition;

        float rot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        return rot;
    }

    private void ResetJoystick()
    {
        TouchManager.Instance.FingerIDs.Remove(cachedTouchIndex);
        cachedTouchIndex = -1;

        isTouching = false;
        isInitiated = false;

        if (currentTapTime > 0)
        {
            if (currentTapTime <= tapDuration) Tap();
            currentTapTime = 0;
        }

        if (isHolding)
        {
            EndHold();
        }
    }

    private void TouchInput()
    {
        if (cachedTouchIndex == -1)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                Vector3 touchPos = touch.position;
                Vector3 localTouchPos = stickRoot.transform.InverseTransformPoint(touchPos);

                if (touch.phase == TouchPhase.Began && localTouchPos.magnitude <= maxDistance)
                {
                    if (TouchManager.Instance.FingerIDs.Contains(i)) continue;
                    isInitiated = true;
                    cachedTouchIndex = touch.fingerId;
                    TouchManager.Instance.FingerIDs.Add(touch.fingerId);
                    break;
                }
            }
        }
        else
        {
            if (cachedTouchIndex >= Input.touchCount)
            {
                ResetJoystick();
                return;
            }

            Touch touch = Input.GetTouch(cachedTouchIndex);

            Vector3 touchPos = touch.position;
            touchPosition = touchPos;

            Vector3 localTouchPos = stickRoot.transform.InverseTransformPoint(touchPos);

            if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
            {
                ResetJoystick();
            }

            JoystickMovement(touchPos, localTouchPos);
            currentHeadings = GetHeadings(touchPos);

            currentDirection = GetDirection(touchPos);
        }
    }

    protected virtual void JoystickMovement(Vector3 pos, Vector3 localPos)
    {
        if (!isInitiated)
        {
            return;
        }

        stickDistance = localPos.magnitude;

        if (stickDistance <= maxDistance)
        {
            stickImage.rectTransform.position = pos;

            currentTapTime += Time.deltaTime;

            isTouching = true;
        }
        else
        {
            Vector3 clampedPos = localPos.normalized * maxDistance;
            clampedPos = stickRoot.transform.TransformPoint(clampedPos);
            stickImage.rectTransform.position = clampedPos;
        }

        if (currentTapTime > holdDuration)
        {
            Hold();
        }
    }

    protected virtual void Tap()
    {

    }

    protected virtual void Hold()
    {
        isHolding = true;
    }

    protected virtual void EndHold()
    {
        isHolding = false;
    }

    public Vector3 GetScaledPosition(Vector3 origin, float scaleMaxDistance)
    {
        return origin + stickImage.rectTransform.localPosition / maxDistance * scaleMaxDistance;
    }
}