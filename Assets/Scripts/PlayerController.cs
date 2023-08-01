using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float minSwipeDistanse = 50f;

    private float swipeDirectionsMin = 0.5f;

    private Finger movementFinger;

    private Vector2 startPosition;

    public static Action moveRight;
    public static Action moveLeft;
    public static Action moveUp;
    public static Action moveDown;

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += SwipeStart;
        ETouch.Touch.onFingerUp += SwipeEnd;
    }

    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= SwipeStart;
        ETouch.Touch.onFingerUp -= SwipeEnd;
        EnhancedTouchSupport.Disable();
    }

    private void SwipeStart(Finger touchedFinger)
    {
        if(movementFinger == null)
        {
            movementFinger = touchedFinger;
            startPosition = touchedFinger.screenPosition;
        }
    }

    private void SwipeEnd(Finger touchedFinger)
    {
        if(touchedFinger == movementFinger)
        {
            Vector2 endPosition = touchedFinger.screenPosition;
            float swipeDistance = (endPosition - startPosition).magnitude;

            if(swipeDistance > minSwipeDistanse)
            {
                Vector2 swipeDirection = (endPosition - startPosition).normalized;
                HandleSwipe(swipeDirection);
            }
        }

        movementFinger = null;
    }

    private void HandleSwipe(Vector2 direction)
    {
        if(direction.x > swipeDirectionsMin)
        {
            moveRight?.Invoke();
        }
        else if(direction.x < -swipeDirectionsMin)
        {
            moveLeft?.Invoke();
        }
        else if( direction.y > swipeDirectionsMin)
        {
            moveUp?.Invoke();
        }
        else if(direction.y < -swipeDirectionsMin)
        {
            moveDown?.Invoke();
        }
        else
        {
            Debug.Log("Invalid Swipe Direction");
        }
    }
}
