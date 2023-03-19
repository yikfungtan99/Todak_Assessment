﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class Joystick : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] protected Image stickImage;
    [SerializeField] protected RectTransform stickRoot;

    [Header("Properties")]
    [SerializeField] protected float maxDistance = 50f; // the maximum distance the thumb can move from the center
    [SerializeField] protected float tapDuration = 0.1f;

    protected Vector3 defaultFingerPosition; // the default position of the thumb image

    private int cachedTouchIndex = -1;

    protected float currentHeadings = 0;

    protected Vector3 currentDirection;

    protected float currentTapTime = 0f;

    protected bool isInitiated;
    protected bool isTouching; // whether the joystick thumb is currently being touched
    protected Vector2 touchPosition;
    public Vector3 Direction { get => currentDirection; }

    protected bool isMoving => !isTouching && !isInitiated;

    void Start()
    {
        // calculate the maximum distance the thumb can move from the center
        maxDistance = stickRoot.sizeDelta.x / 2f - stickImage.rectTransform.sizeDelta.x / 2f;
        // save the default position of the thumb image
        defaultFingerPosition = stickImage.rectTransform.position;
    }

    protected virtual void Update()
    {
        TouchInput();

        if (!isTouching)
        {
            // reset the position of the thumb image to the center of the joystick background image
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

    private void MouseInput()
    {
        Vector3 mousePos = Vector3.zero;

        if (Input.GetMouseButton(0))
        {
            mousePos = Input.mousePosition;
            Vector3 localMousePos = stickRoot.transform.InverseTransformPoint(mousePos);

            if (localMousePos.magnitude <= maxDistance)
            {
                isInitiated = true;
            }

            JoystickMovement(mousePos, localMousePos);

            currentHeadings = GetHeadings(mousePos);
        }
        else
        {
            ResetJoystick();
        }

        currentDirection = GetDirection(mousePos);
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
            Touch touch = Input.GetTouch(cachedTouchIndex);

            Vector3 touchPos = touch.position;
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

        // check if the touch is within the bounds of the joystick background image
        if (localPos.magnitude <= maxDistance)
        {
            // move the thumb image to the touch position
            stickImage.rectTransform.position = pos;

            currentTapTime += Time.deltaTime;

            Hold();

            isTouching = true;

            touchPosition = pos;
        }
        else
        {
            // calculate the position of the thumb image at the edge of the joystick background image
            Vector3 clampedPos = localPos.normalized * maxDistance;
            // convert the clamped position back to world coordinates
            clampedPos = stickRoot.transform.TransformPoint(clampedPos);
            // move the thumb image to the clamped position
            stickImage.rectTransform.position = clampedPos;
        }
    }

    protected virtual void Tap()
    {

    }

    protected virtual void Hold()
    {

    }
}