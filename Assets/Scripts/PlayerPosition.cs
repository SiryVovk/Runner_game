using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    [Header("Lane")]
    [SerializeField] private float laneWidth = 2f;
    [SerializeField] private float changeLaneDuration = 80f;
    [Header("Speed")]
    [SerializeField] private float forwardSpeed = 5;
    [SerializeField] private float speedIncreaseStep = 0.01f;
    [Header("Jump")]
    [SerializeField] private float jumpForce = 10f;

    private float maxSpeed = 10;

    private float slideDuration = 1.5f;

    private float normalColliderYSize = 2;
    private float normalColliderYCenter = 1;
    private float slidColliderYSize = 1;
    private float slidColliderYCenter = 0.5f;

    private int playerLane = 2;
    private int maxLane = 3;
    private int minLane = 1;

    private bool onGround = true;
    private bool isSliding = false;

    private string roadTag = "Road";

    private Rigidbody playerRB;
    private BoxCollider playerCollider;

    private void OnEnable()
    {
        SwipeController.moveRight += MoveRight;
        SwipeController.moveLeft += MoveLeft;
        SwipeController.moveUp += MoveUp;
        SwipeController.moveDown += MoveDown;
    }

    private void OnDisable()
    {
        SwipeController.moveRight -= MoveRight;
        SwipeController.moveLeft -= MoveLeft;
        SwipeController.moveUp -= MoveUp;
        SwipeController.moveDown -= MoveDown;
    }

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
        playerCollider = GetComponent<BoxCollider>();
    }



    private void Update()
    {
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
        if(forwardSpeed < maxSpeed)
        {
            forwardSpeed += (speedIncreaseStep * Time.deltaTime);
        }
    }

    private void MoveRight()
    {
        if(playerLane < maxLane)
        {
            playerLane++;
            ChangeLane(Vector3.right);
        }
    }

    private void MoveLeft()
    {
        if(playerLane > minLane)
        {
            playerLane--;
            ChangeLane(Vector3.left);
        }
    }

    private void ChangeLane(Vector3 direction)
    {
        Vector3 move = direction * laneWidth;
        Vector3 newPosition = transform.position + direction;
        transform.position = Vector3.Lerp(transform.position,newPosition,changeLaneDuration * Time.deltaTime);
    }

    private void MoveUp()
    {
        if(onGround && !isSliding)
        {
            onGround = false;
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void MoveDown()
    {
        if (!isSliding && onGround)
        {
            StartCoroutine(SlideStart());
        }
    }

    private IEnumerator SlideStart()
    {
        isSliding = true;

        ChangeCollider(slidColliderYSize, slidColliderYCenter);

        yield return new WaitForSeconds(slideDuration / Time.timeScale);

        ChangeCollider(normalColliderYSize, normalColliderYCenter);

         isSliding = false;
        
    }

    private void ChangeCollider(float colliderYSize, float colliderYCenter)
    {
        Vector3 newColliderSize = playerCollider.size;
        newColliderSize.y = colliderYSize;
        playerCollider.size = newColliderSize;
        Vector3 newColliderCenter = playerCollider.center;
        newColliderCenter.y = colliderYCenter;
        playerCollider.center = newColliderCenter;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(roadTag))
        {
            onGround = true;
        }
    }

}
