using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject startText;
    [SerializeField] private GameObject gameOverText;

    private void OnEnable()
    {
        SwipeController.firstTap += GameStart;
        PlayerCollisions.gameOver += GameOver;
    }

    private void OnDisable()
    {
        SwipeController.firstTap -= GameStart;
        PlayerCollisions.gameOver -= GameOver;
    }

    private void GameStart()
    {
        startText.SetActive(false);
    }

    private void GameOver()
    {
        gameOverText.SetActive(true);
    }
}
