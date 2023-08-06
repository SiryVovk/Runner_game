using UnityEngine;
using Firebase;
using Firebase.Auth;
using TMPro;
using System.Collections;
using System.ComponentModel;

public class AuthenticationManager : MonoBehaviour
{
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser user;

    [Header("Login")]
    [SerializeField] private TMP_InputField loginEmail;
    [SerializeField] private TMP_InputField loginPassword;
    [SerializeField] private TMP_Text warningLogText;
    [SerializeField] private TMP_Text confirmLogText;

    [Header("Register")]
    [SerializeField] private TMP_InputField registerEmail;
    [SerializeField] private TMP_InputField registerUserName;
    [SerializeField] private TMP_InputField registerPassword;
    [SerializeField] private TMP_InputField registerConfirmPassword;
    [SerializeField] private TMP_Text warningRegText;

    private void Awake()
    {

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if(dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Fierbace dependency" + dependencyStatus);
            }
        });
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Fierbase Auth");

        auth = FirebaseAuth.DefaultInstance;
    }

    public void LoginButton()
    {
        StartCoroutine(Login(loginEmail.text, loginPassword.text));
    }

    public void RegiserButton()
    {
        StartCoroutine(Register(registerEmail.text, registerUserName.text, registerPassword.text));
    }

    private IEnumerator Login(string email, string password)
    {
        var loginTask = auth.SignInWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(predicate: () => loginTask.IsCompleted);

        if(loginTask.Exception != null)
        {
            Debug.LogWarning(message: "Failed to register task with " + loginTask.Exception);
            FirebaseException firebaseException = loginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseException.ErrorCode;

            string message = "Login Failed";

            switch(errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account dose not exist";
                    break;
            }
            warningLogText.text = message;
        }
        else
        {
            user = loginTask.Result.User;
            Debug.LogFormat("User signed is successfully: {0} ({1})", user.DisplayName, user.Email);
            AuthenticationUI.instance.StartMainScene();
        }
    }

    private IEnumerator Register(string email, string userName, string password)
    {
        if(userName == "")
        {
            warningLogText.text = "Missing User Name";
        }
        else if(password != registerConfirmPassword.text)
        {
            warningLogText.text = "Password Does Not Match";
        }
        else
        {
            var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);

            yield return new WaitUntil(predicate: () => registerTask.IsCompleted);

            if(registerTask.Exception != null)
            {
                Debug.LogWarning(message: "Failed to register task with " + registerTask.Exception);
                FirebaseException firebaseException = registerTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseException.ErrorCode;

                string message = "Register Failed";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }
                warningRegText.text = message;
            }
            else
            {
                user = registerTask.Result.User;

                if(user != null)
                {
                    UserProfile userProfile = new UserProfile { DisplayName = userName };

                    var userProfileTask = user.UpdateUserProfileAsync(userProfile);

                    yield return new WaitUntil(predicate: () => userProfileTask.IsCompleted);

                    if(userProfileTask.Exception != null)
                    {
                        Debug.LogWarning(message: "Failed to register task with " + userProfileTask.Exception);
                        FirebaseException firebaseException = userProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseException.ErrorCode;
                        warningRegText.text = "UserName Set Failed";
                    }
                    else
                    {
                        AuthenticationUI.instance.StartMainScene();
                    }
                }
            }
        }
    }

}
