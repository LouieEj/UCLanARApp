using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoginHandler: MonoBehaviour
{
    // SerializeField tag allows things to be seen and assigned in the editor, but keeps variables private
    [SerializeField] private TMP_InputField emailInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private Button loginButton;
    [SerializeField] private TMP_Text errorText;
    [SerializeField] private Button registerButton;


    private string emailAddress;
    private string password;

    //Starts the screen in correct orientation
    private void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        loginButton.onClick.AddListener(Login_Attempt); //when the login button is clicked
        registerButton.onClick.AddListener(Register);
    }


    private void Register()
    {
        SceneManager.LoadScene("RegisterScene");
    }


    private void Login_Attempt()
    {
        emailAddress = emailInput.text;
        password = passwordInput.text;

        // checks if username and password are valid
        if (ValidateCredentials(emailAddress, password))
        {
            SceneManager.LoadScene("HomeScreen");
            // load next scene or do other actions here
        }
        else
        {
            errorText.enabled = true;
            // show error message or do other actions here
        }
    }

    private bool ValidateCredentials(string username, string password)
    {
        // replace this with user login
        //Link up to database of user data

        return username == "admin" && password == "password";
    }
}
