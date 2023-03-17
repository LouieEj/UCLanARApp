using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.IO;
using System.Linq;
using ARLocation;

public class LoginHandler: MonoBehaviour
{
    // SerializeField tag allows things to be seen and assigned in the editor, but keeps variables private
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private Button loginButton;
    [SerializeField] private TMP_Text errorText;
    [SerializeField] private Button registerButton;

    private string username;
    private string password;

    //save this data to be accessed by other scenes
    public static int colourBlindSetting = 0;
    public static bool dyslexicSetting = false;


    private List<string> readUsernames = new List<string>();
    private List<string> readPasswords = new List<string>();
    private List<int> readColourBlindSettings = new List<int>();
    private List<bool> readDyslexicSettings = new List<bool>();


    //Starts the screen in correct orientation
    private void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        loginButton.onClick.AddListener(Login_Attempt); //when the login button is clicked
        registerButton.onClick.AddListener(Register);


        TextAsset databaseTextAsset = Resources.Load<TextAsset>("database");
        string databaseText = databaseTextAsset.text;
        string[] databaseValues = databaseText.Split('\n');
        for (int i = 0; i < databaseValues.Length; i++)
        {
            if (!string.IsNullOrEmpty(databaseValues[i]))
            {
                string line = databaseValues[i];
                string[] values = line.Split(',');
                readUsernames.Add(values[0]);
                readPasswords.Add(values[1]);
                readColourBlindSettings.Add(Convert.ToInt32(values[2]));
                readDyslexicSettings.Add(Convert.ToBoolean(values[3]));
            }
        }
    }


    private void Register()
    {
        SceneManager.LoadScene("RegisterScene");
    }


    private void Login_Attempt()
    {
        username = usernameInput.text;
        password = passwordInput.text;

        // checks if username and password are valid
        if (ValidateCredentials(username, password))
        {
            SceneManager.LoadScene("HomeScreen");
            // load next scene or do other actions here
        }
        else
        {
            errorText.enabled = true;
            // show error message when invalid credentials inputted
        }
    }

    private bool ValidateCredentials(string username, string password)
    {
        bool result = false;

        for (int i = 0; i < readUsernames.Count; i++)
        {
            if (readUsernames[i] == username && readPasswords[i] == password)
            {
                colourBlindSetting = readColourBlindSettings[i];
                dyslexicSetting = readDyslexicSettings[i];
                result = true;
                break;
            }
        }

        return result;
    }
}
