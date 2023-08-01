using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    [SerializeField] private float laneWidth = 2f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float forwardSpeed = 5;

    private float maxSpeed;
    private float slideDuration = 1.5f;
    private float colliderCenterChange = 0.5f;

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
        PlayerController.moveRight += MoveRight;
        PlayerController.moveLeft += MoveLeft;
        PlayerController.moveUp += MoveUp;
        PlayerController.moveDown += MoveDown;
    }

    private void OnDisable()
    {
        PlayerController.moveRight -= MoveRight;
        PlayerController.moveLeft -= MoveLeft;
        PlayerController.moveUp -= MoveUp;
        PlayerController.moveDown -= MoveDown;
    }

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
        playerCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
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
        transform.position = newPosition;
    }

    private void MoveUp()
    {
        if(onGround)
        {
            onGround = false;
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void MoveDown()
    {
        if (!isSliding)
        {
            StartCoroutine(SlideStart());
        }
    }

    private IEnumerator SlideStart()
    {
        isSliding = true;
        Vector3 newCenter = playerCollider.center;
        newCenter.y -= colliderCenterChange;
        playerCollider.size = newCenter;
        yield return new WaitForSeconds(slideDuration / Time.timeScale);
        isSliding = false;
        newCenter = playerCollider.center;
        newCenter.y += colliderCenterChange;
        playerCollider.center = newCenter;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(roadTag))
        {
            onGround = true;
        }
    }

}
