using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RegisterHandler : MonoBehaviour
{
    // SerializeField tag allows things to be seen and assigned in the editor, but keeps variables private
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_InputField emailInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private Button registerButton;
    [SerializeField] private TMP_Text errorText;
    [SerializeField] private Button loginButton;

    void Start()
    {
        registerButton.onClick.AddListener(Register);
        loginButton.onClick.AddListener(Login);
    }

    private void Login()
    {
        SceneManager.LoadScene("LoginScene");
    }

    private void Register()
    {
        if (string.IsNullOrEmpty(nameInput.text))
        {
            errorText.text = "Please enter your name!";
            errorText.enabled = true;
        }
        else if (string.IsNullOrEmpty(emailInput.text))
        {
            errorText.text = "Please enter your email address!";
            errorText.enabled = true;
        }
        else if (string.IsNullOrEmpty(passwordInput.text))
        {
            errorText.text = "Please enter a password!";
            errorText.enabled = true;
        }
        else
        {
            SceneManager.LoadScene("HomeScreen");
        }
    }
}
