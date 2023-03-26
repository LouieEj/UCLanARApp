using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RegisterHandler : MonoBehaviour
{
    // SerializeField tag allows things to be seen and assigned in the editor, but keeps variables private
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private Button registerButton;
    [SerializeField] private TMP_Text errorText;
    [SerializeField] private TMP_Dropdown colourBlindDropdown;
    [SerializeField] private TMP_Dropdown dyslexicDropDown;
    [SerializeField] private Button loginButton;

    private List<string> readUsernames = new List<string>();
    private List<string> readPasswords = new List<string>();
    private List<int> readColourBlindSettings = new List<int>();
    private List<bool> readDyslexicSettings = new List<bool>();


    void Start()
    {
        registerButton.onClick.AddListener(Register);
        loginButton.onClick.AddListener(Login);

        string basePath = Path.Combine(Application.persistentDataPath, "database.csv");
        try
        {
            string databaseText = File.ReadAllText(basePath);
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
        catch
        {
            File.WriteAllText(basePath, "");
        }

    }

    private void Login()
    {
        SceneManager.LoadScene("LoginScene");
    }

    private void Register()
    {
        if (string.IsNullOrEmpty(usernameInput.text))
        {
            errorText.text = "Please enter a username!";
            errorText.enabled = true;
        }
        else if (string.IsNullOrEmpty(passwordInput.text))
        {
            errorText.text = "Please enter a password!";
            errorText.enabled = true;
        }
        else //all details correct, check username isn't already registered
        {
            if (readUsernames.Count == 0)
            {
                AttemptRegister();
                return;
            }
            for (int i = 0; i < readUsernames.Count; i++)
            {
                if (readUsernames[i] == usernameInput.text && readPasswords[i] != passwordInput.text) //username already exists, wrong password
                {
                    errorText.text = "Username already exists!";
                    errorText.enabled = true;
                    return;
                }
                else if(readUsernames[i] == usernameInput.text && readPasswords[i] == passwordInput.text) //username already exists and correct password, login
                {
                    LoginHandler.user = usernameInput.text;
                    LoginHandler.colourBlindSetting = readColourBlindSettings[i];
                    LoginHandler.dyslexicSetting = readDyslexicSettings[i];
                    SceneManager.LoadScene("HomeScreen");
                    return;
                }
                else
                {
                    AttemptRegister();
                }
            }
        }
    }

    private void AttemptRegister()
    {
        string basePath = Path.Combine(Application.persistentDataPath, "database.csv");
        Debug.Log(basePath);
        try
        {
            StreamWriter writer = new StreamWriter(basePath, true);
            string colourBlindSetting = "0";
            string dyslexicSetting = "FALSE";
            colourBlindSetting = colourBlindDropdown.value.ToString();
            if (dyslexicDropDown.value == 1)
            {
                dyslexicSetting = "TRUE";
            }
            string line = usernameInput.text + "," + passwordInput.text + "," + colourBlindSetting + "," + dyslexicSetting;
            writer.WriteLine(line);
            writer.Close();
            LoginHandler.user = usernameInput.text;
            LoginHandler.colourBlindSetting = Convert.ToInt32(colourBlindSetting);
            LoginHandler.dyslexicSetting = Convert.ToBoolean(dyslexicSetting);
            SceneManager.LoadScene("HomeScreen");
            return;
        }
        catch
        {
            errorText.text = "Cannot find database file in directory: " + basePath;
            errorText.enabled = true;
        }
    }
}
