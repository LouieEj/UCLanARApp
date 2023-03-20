using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Wilberforce;

public class SettingsHandler : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown colourBlindDropdown;
    [SerializeField] private TMP_Dropdown dyslexicDropDown;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button backButton;

    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text colourblindTitleText;
    [SerializeField] private TMP_Text successText;
    [SerializeField] private TMP_Text dyslexicTitleText;

    [SerializeField] private Camera cam;

    [SerializeField] private TMP_FontAsset dyslexicFont;
    [SerializeField] private TMP_FontAsset regularTitleFont;
    [SerializeField] private TMP_FontAsset regularFont;

    [SerializeField] private Button deleteAccountButton;
    [SerializeField] private GameObject confirmationPanel;
    [SerializeField] private TMP_Text confirmationText;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    private void Start()
    {
        backButton.onClick.AddListener(() => { SceneManager.LoadScene("HomeScreen"); });
        confirmButton.onClick.AddListener(UpdateDetails);
        deleteAccountButton.onClick.AddListener(ClickDeleteAccount);
        yesButton.onClick.AddListener(DeleteAccount);
        noButton.onClick.AddListener(CancelDeleteAccount);

        colourBlindDropdown.value = LoginHandler.colourBlindSetting;
        dyslexicDropDown.value = (LoginHandler.dyslexicSetting) ? 1 : 0;
        cam.GetComponent<Colorblind>().Type = LoginHandler.colourBlindSetting;
        if (LoginHandler.dyslexicSetting)
        {
            titleText.font = dyslexicFont;
            colourblindTitleText.font = dyslexicFont;
            successText.font = dyslexicFont;
            dyslexicTitleText.font = dyslexicFont;
            confirmButton.GetComponentInChildren<TMP_Text>().font = dyslexicFont;
            backButton.GetComponentInChildren<TMP_Text>().font = dyslexicFont;
            colourBlindDropdown.GetComponentInChildren<TMP_Text>().font = dyslexicFont;
            dyslexicDropDown.GetComponentInChildren<TMP_Text>().font = dyslexicFont;
            confirmationText.font = dyslexicFont;
            deleteAccountButton.GetComponentInChildren<TMP_Text>().font = dyslexicFont;
            yesButton.GetComponentInChildren<TMP_Text>().font = dyslexicFont;
            noButton.GetComponentInChildren<TMP_Text>().font = dyslexicFont;
        }
    }


    void UpdateDetails()
    {
        string basePath = Path.Combine(Application.persistentDataPath, "database.csv");
        string databaseText = File.ReadAllText(basePath);
        string[] databaseValues = databaseText.Split('\n');
        List<string[]> databaseLines = new List<string[]>();
        for (int i = 0; i < databaseValues.Length; i++)
        {
            if (!string.IsNullOrEmpty(databaseValues[i])){
                string dyslexicSetting = "FALSE";
                string[] line = databaseValues[i].Split(',');
                if (line[0] == LoginHandler.user)
                {
                    line[2] = colourBlindDropdown.value.ToString();
                    if (dyslexicDropDown.value == 1)
                    {
                        dyslexicSetting = "TRUE";
                    }
                    line[3] = dyslexicSetting;
                    LoginHandler.colourBlindSetting = Convert.ToInt32(line[2]);
                    LoginHandler.dyslexicSetting = Convert.ToBoolean(dyslexicSetting);
                }
                databaseLines.Add(line);
            }
        }

        string lines = "";
        foreach (string[] line in databaseLines)
        {
            lines += line[0] + "," + line[1] + "," + line[2] + "," + line[3] + "\n";
        }
        File.WriteAllText(basePath, lines);
        successText.enabled = true;

        colourBlindDropdown.value = LoginHandler.colourBlindSetting;
        dyslexicDropDown.value = (LoginHandler.dyslexicSetting) ? 1 : 0;
        cam.GetComponent<Colorblind>().Type = LoginHandler.colourBlindSetting;
        if (LoginHandler.dyslexicSetting)
        {
            titleText.font = dyslexicFont;
            colourblindTitleText.font = dyslexicFont;
            successText.font = dyslexicFont;
            dyslexicTitleText.font = dyslexicFont;
            confirmButton.GetComponentInChildren<TMP_Text>().font = dyslexicFont;
            backButton.GetComponentInChildren<TMP_Text>().font = dyslexicFont;
            colourBlindDropdown.GetComponentInChildren<TMP_Text>().font = dyslexicFont;
            dyslexicDropDown.GetComponentInChildren<TMP_Text>().font = dyslexicFont;
            confirmationText.font = dyslexicFont;
            deleteAccountButton.GetComponentInChildren<TMP_Text>().font = dyslexicFont;
            yesButton.GetComponentInChildren<TMP_Text>().font = dyslexicFont;
            noButton.GetComponentInChildren<TMP_Text>().font = dyslexicFont;
        }
        else
        {
            titleText.font = regularTitleFont;
            colourblindTitleText.font = regularFont;
            successText.font = regularFont;
            dyslexicTitleText.font = regularFont;
            confirmButton.GetComponentInChildren<TMP_Text>().font = regularFont;
            backButton.GetComponentInChildren<TMP_Text>().font = regularFont;
            colourBlindDropdown.GetComponentInChildren<TMP_Text>().font = regularFont;
            dyslexicDropDown.GetComponentInChildren<TMP_Text>().font = regularFont;
            confirmationText.font = regularFont;
            deleteAccountButton.GetComponentInChildren<TMP_Text>().font = regularFont;
            yesButton.GetComponentInChildren<TMP_Text>().font = regularFont;
            noButton.GetComponentInChildren<TMP_Text>().font = regularFont;
        }
    }

    void ClickDeleteAccount()
    {
        confirmationPanel.SetActive(true);
    }

    void CancelDeleteAccount()
    {
        confirmationPanel.SetActive(false);
    }

    private IEnumerator DelayForSceneChange()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("LoginScene");
    }

    void DeleteAccount()
    {
        string basePath = Path.Combine(Application.persistentDataPath, "database.csv");
        string databaseText = File.ReadAllText(basePath);
        string[] databaseValues = databaseText.Split('\n');
        List<string[]> databaseLines = new List<string[]>();
        for (int i = 0; i < databaseValues.Length; i++)
        {
            if (!string.IsNullOrEmpty(databaseValues[i]))
            {
                string[] line = databaseValues[i].Split(',');
                if (line[0] != LoginHandler.user)
                {
                    databaseLines.Add(line);
                }
            }
        }

        string lines = "";
        foreach (string[] line in databaseLines)
        {
            lines += line[0] + "," + line[1] + "," + line[2] + "," + line[3] + "\n";
        }
        File.WriteAllText(basePath, lines);
        successText.text = "Account deleted! You will shortly return to the login screen.";
        successText.enabled = true;
        confirmationPanel.SetActive(false);
        StartCoroutine(DelayForSceneChange());
    }
}
