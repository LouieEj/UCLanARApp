using System.Collections;
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


    private string emailAddress;
    private string password;

    //Starts the screen in correct orientation
    private void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }


    public void Login_Attempt()
    {
        loginButton.onClick.AddListener(() => //when the login button is clicked
        {
            emailAddress = emailInput.text;
            password = passwordInput.text;

            // checks if username and password are valid
            if (ValidateCredentials(emailAddress, password))
            {
                Debug.Log("Login Successful");
                SceneManager.LoadScene("HomeScreen");
                // load next scene or do other actions here
            }
            else
            {
                Debug.Log("Login Failed");
                errorText.enabled = true;
                // show error message or do other actions here
            }
        });
    }

    private bool ValidateCredentials(string username, string password)
    {
        // replace this with user login
        //Link up to database of user data
        return username == "admin" && password == "password";
    }
}
