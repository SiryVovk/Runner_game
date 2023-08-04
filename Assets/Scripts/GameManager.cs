using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void OnEnable()
    {
        SwipeController.firstTap += StartGame;
        PlayerCollisions.gameOver += GameOver;
    }

    private void OnDisable()
    {
        SwipeController.firstTap -= StartGame;
        PlayerCollisions.gameOver -= GameOver;
    }

    private void Start()
    {
        Time.timeScale = 0;
    }

    private void StartGame()
    {
        Time.timeScale = 1;
    }

    private void GameOver()
    {
        Time.timeScale = 0;
    }
}
