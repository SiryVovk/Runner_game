using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIController : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] private GameObject startText;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("UI")]
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject inGame;

    [Header("GameManager")]
    [SerializeField] private GameManager gameManager;

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

    private void Update()
    {
        scoreText.text = Mathf.FloorToInt(gameManager.Score).ToString();
    }

    private void GameStart()
    {
        startText.SetActive(false);
    }

    private void GameOver()
    {
        gameOverText.SetActive(true);
    }

    public void MenuButton()
    {
        menu.SetActive(true);
        inGame.SetActive(false);
    }

    public void ReturnButton()
    {
        menu.SetActive(false);
        inGame.SetActive(true);
    }

    public void LogOut()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}
