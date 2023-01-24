using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginHandler : MonoBehaviour
{
    // SerializeField tag allows things to be seen and assigned in the editor, but keeps variables private
    [SerializeField] private TMP_InputField emailInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private Button loginButton;
    [SerializeField] private TMP_Text errorText;

    private string emailAddress;
    private string password;

    // Start called when script is enabled before Update method
    private void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    // Awake called before Update method, even when script is disabled
    // Anything that could cause an error if not initalised should go in Awake
    private void Awake()
    {
        loginButton.onClick.AddListener(Login);
    }

    // Update is called once per frame
    private void Update()
    {

    }

    private void Login()
    {
        emailAddress = emailInput.text;
        password = passwordInput.text;

        if (emailAddress == "" || password == "") errorText.enabled = true;
        else SceneManager.LoadScene("HomeScreen");
    }
}
