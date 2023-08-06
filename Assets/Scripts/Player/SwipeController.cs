using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class SwipeController : MonoBehaviour
{
    [SerializeField] private float minSwipeDistanse = 50f;

    private float swipeDistanceMin = 0.5f;
    private bool isGameStarted = false;
    private bool isGameOver = false;

    private Finger movementFinger;

    private Vector2 startPosition;

    public static Action moveRight;
    public static Action moveLeft;
    public static Action moveUp;
    public static Action moveDown;

    public static Action firstTap;

    private void OnEnable()
    {
        PlayerCollisions.gameOver += SetGameOver;
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += SwipeStart;
        ETouch.Touch.onFingerUp += SwipeEnd;
    }

    private void OnDisable()
    {
        PlayerCollisions.gameOver -= SetGameOver;
        ETouch.Touch.onFingerDown -= SwipeStart;
        ETouch.Touch.onFingerUp -= SwipeEnd;
        EnhancedTouchSupport.Disable();
    }

    private void SetGameOver()
    {
        isGameOver = true;
    }

    private void SwipeStart(Finger touchedFinger)
    {
        if(movementFinger == null && isGameStarted)
        {
            movementFinger = touchedFinger;
            startPosition = touchedFinger.screenPosition;
        }

        if (!isGameStarted)
        {
            isGameStarted = true;
            firstTap?.Invoke();
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
        if (!isGameOver)
        {
            if (direction.x > swipeDistanceMin)
            {
                moveRight?.Invoke();
            }
            else if (direction.x < -swipeDistanceMin)
            {
                moveLeft?.Invoke();
            }
            else if (direction.y > swipeDistanceMin)
            {
                moveUp?.Invoke();
            }
            else if (direction.y < -swipeDistanceMin)
            {
                moveDown?.Invoke();
            }
        }
    }
}
