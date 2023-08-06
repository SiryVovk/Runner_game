using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AuthenticationUI : MonoBehaviour
{
    public static AuthenticationUI instance;

    [SerializeField] private GameObject logIn;
    [SerializeField] private GameObject register;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }
    public void LoginScreen()
    {
        logIn.SetActive(true);
        register.SetActive(false);
    }

    public void RegisterScreen()
    {
        logIn.SetActive(false);
        register.SetActive(true);
    }

    public void StartMainScene()
    {
        SceneManager.LoadScene(1);
    }
}
