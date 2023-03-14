using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
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

        string filePath = Application.dataPath + "/database.csv";
        StreamReader reader = new StreamReader(filePath);
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            string[] values = line.Split(',');
            readUsernames.Add(values[0]);
            readUsernames.Add(values[1]);
            readColourBlindSettings.Add(Convert.ToInt32(values[2]));
            readDyslexicSettings.Add(Convert.ToBoolean(values[3]));
        }
        reader.Close();
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
            for (int i = 0; i < readUsernames.Count; i++)
            {
                if (readUsernames[i] == usernameInput.text && readPasswords[i] != passwordInput.text) //username already exists, wrong password
                {
                    errorText.text = "Username already exists!";
                    errorText.enabled = true;
                }
                else if(readUsernames[i] == usernameInput.text && readPasswords[i] == passwordInput.text) //username already exists and correct password, login
                {
                    LoginHandler.colourBlindSetting = readColourBlindSettings[i];
                    LoginHandler.dyslexicSetting = readDyslexicSettings[i];
                    SceneManager.LoadScene("HomeScreen");
                }
                else
                {
                    string filePath = Application.dataPath + "/database.csv";
                    string colourBlindSetting = "0";
                    string dyslexicSetting = "FALSE";
                    StreamWriter writer = new StreamWriter(filePath, true);
                    colourBlindSetting = colourBlindDropdown.value.ToString();
                    if (dyslexicDropDown.value == 1)
                    {
                        dyslexicSetting = "TRUE";
                    }
                    string line = usernameInput.text + "," + passwordInput.text + "," + colourBlindSetting + "," + dyslexicSetting;
                    writer.WriteLine(line);
                    writer.Close();
                    SceneManager.LoadScene("HomeScreen");
                    break;
                }
            }
        }
    }
}
