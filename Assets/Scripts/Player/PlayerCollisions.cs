using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    private PlayerPosition player;

    private string roadTag = "Road";
    private string obstacleTag = "Obstacle";

    public static Action gameOver;

    private void Awake()
    {
        player = GetComponent<PlayerPosition>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(roadTag))
        {
            player.SetOnGround();
        }
        else if(collision.gameObject.CompareTag(obstacleTag))
        {
            gameOver?.Invoke();
        }
    }
}
