using System;
using UnityEngine;
using UnityEngine.UI;

public class MovementJoystickController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Image innerStick;
    [SerializeField] private Image outerStick;
    [SerializeField] private Transform headingsPivot;

    [Header("Properties")]
    [SerializeField] private float maxDistance = 50f; // the maximum distance the thumb can move from the center

    private Vector3 defaultThumbPosition; // the default position of the thumb image

    private Vector3 currentDirection;
    private float currentHeadings = 0;
    public Vector3 Direction { get => currentDirection; }

    private bool isInitiated;
    private bool isTouching; // whether the joystick thumb is currently being touched
    private Vector2 touchPosition;

    void Start()
    {
        // calculate the maximum distance the thumb can move from the center
        maxDistance = outerStick.rectTransform.sizeDelta.x / 2f - innerStick.rectTransform.sizeDelta.x / 2f;
        // save the default position of the thumb image
        defaultThumbPosition = innerStick.rectTransform.position;
    }

    void Update()
    {
#if PLATFORM_ANDROID && !UNITY_EDITOR
        // check for touch input
        TouchInput();
#endif
        // check for mouse input
#if UNITY_EDITOR
        MouseInput();
#endif

        UpdateHeadings();

        if (!isTouching)
        {
            // reset the position of the thumb image to the center of the joystick background image
            innerStick.rectTransform.position = defaultThumbPosition;
        }
    }

    private void UpdateHeadings()
    {
        if (!isInitiated) return;

        Vector3 curDir = headingsPivot.rotation.eulerAngles;
        headingsPivot.rotation = Quaternion.Euler(new Vector3(curDir.x, curDir.y, currentHeadings));
    }

    private void MouseInput()
    {
        Vector3 mousePos = Vector3.zero;

        if (Input.GetMouseButton(0))
        {
            mousePos = Input.mousePosition;
            Vector3 localMousePos = outerStick.transform.InverseTransformPoint(mousePos);

            if (localMousePos.magnitude <= maxDistance)
            {
                isInitiated = true;
            }

            HandleMovementInput(mousePos, localMousePos);

            currentHeadings = GetHeadings(mousePos);
        }
        else
        {
            isTouching = false;
            isInitiated = false;
        }

        currentDirection = GetDirection(mousePos);
    }

    private void TouchInput()
    {
        Vector3 touchPos = Vector3.zero;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPos = touch.position;

            // convert the touch position to local coordinates of the joystick background image
            Vector3 localTouchPos = outerStick.transform.InverseTransformPoint(touchPos);

            if (touch.phase == TouchPhase.Began && localTouchPos.magnitude <= maxDistance)
            {
                isInitiated = true;
            }

            HandleMovementInput(touchPos, localTouchPos);

            currentHeadings = GetHeadings(touchPos);
        }
        else
        {
            isTouching = false;
            isInitiated = false;
        }

        currentDirection = GetDirection(touchPos);
    }

    private void HandleMovementInput(Vector3 pos, Vector3 localPos)
    {
        if (!isInitiated)
        {
            return;
        }

        // check if the touch is within the bounds of the joystick background image
        if (localPos.magnitude <= maxDistance)
        {
            // move the thumb image to the touch position
            innerStick.rectTransform.position = pos;
            isTouching = true;
            touchPosition = pos;
        }
        else
        {
            // calculate the position of the thumb image at the edge of the joystick background image
            Vector3 clampedPos = localPos.normalized * maxDistance;
            // convert the clamped position back to world coordinates
            clampedPos = outerStick.transform.TransformPoint(clampedPos);
            // move the thumb image to the clamped position
            innerStick.rectTransform.position = clampedPos;
        }
    }

    private float GetHeadings(Vector2 pos)
    {
        Vector2 direction = pos - (Vector2)defaultThumbPosition;

        float rot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        return rot;
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
}