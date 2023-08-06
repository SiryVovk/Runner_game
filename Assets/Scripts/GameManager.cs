using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float score = 0;

    public float Score 
    { 
        get { return score; }
        private set { score = value; }
    }

    private void OnEnable()
    {
        SwipeController.firstTap += StartGame;
        PlayerCollisions.gameOver += StopGame;
    }

    private void OnDisable()
    {
        SwipeController.firstTap -= StartGame;
        PlayerCollisions.gameOver -= StopGame;
    }

    private void Start()
    {
        Time.timeScale = 0;
    }

    private void Update()
    {
        score += Time.deltaTime;    
    }

    public void StartGame()
    {
        Time.timeScale = 1;
    }

    public void StopGame()
    {
        Time.timeScale = 0;
    }
}
