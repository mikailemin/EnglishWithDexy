using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthManager : MonoBehaviour
{
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public Button loginButton;
    public Button signUpButton;
    public TextMeshProUGUI feedbackText;



    private FirebaseAuth auth;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;

        loginButton.onClick.AddListener(() => Login(emailInput.text, passwordInput.text));
        signUpButton.onClick.AddListener(() => SignUp(emailInput.text, passwordInput.text));
    }

    void Login(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                feedbackText.text = "Login Failed: " + task.Exception;
                return;
            }

            // AuthResult result = task.Result;
            FirebaseUser user = auth.CurrentUser /*result.User*/;

            feedbackText.text = "Login Successful: " + user.Email;

            Debug.Log(email + "kullanıcı giriş yaptı");

            SceneManager.LoadScene("questscene");
            // Kullanıcı başarılı bir şekilde giriş yaptıktan sonra kelime ekleme ekranına yönlendirin.
        });
    }

    void SignUp(string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                feedbackText.text = "Sign Up Failed: " + task.Exception;
                return;
            }
            AuthResult result = task.Result;
            FirebaseUser user = result.User;

            feedbackText.text = "Sign Up Successful: " + user.Email;
            Debug.Log(email + "kullanıcı eklendi");
            SceneManager.LoadScene("questscene");
            // Kullanıcı başarılı bir şekilde kayıt olduktan sonra kelime ekleme ekranına yönlendirin.
        });
    }
}
