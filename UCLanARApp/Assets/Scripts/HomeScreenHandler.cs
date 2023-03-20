using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Wilberforce;

public class HomeScreenHandler : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] Button settingsButton;
    [SerializeField] Button logoutButton;
    [SerializeField] TMP_Text homeScreenText;
    [SerializeField] TMP_Text startButtonText;
    [SerializeField] TMP_Text settingsButtonText;
    [SerializeField] TMP_Text logoutButtonText;
    [SerializeField] TMP_FontAsset dyslexicFont;
    [SerializeField] Camera cam;

    void Start()
    {
        startButton.onClick.AddListener(() => { SceneManager.LoadScene("ARScene"); });
        settingsButton.onClick.AddListener(() => { SceneManager.LoadScene("SettingsScene"); });
        logoutButton.onClick.AddListener(() => { SceneManager.LoadScene("LoginScene"); });   
        if (LoginHandler.dyslexicSetting)
        {
            homeScreenText.font = dyslexicFont;
            startButtonText.font = dyslexicFont;
            settingsButtonText.font = dyslexicFont;
            logoutButtonText.font = dyslexicFont;
        }
        cam.GetComponent<Colorblind>().Type = LoginHandler.colourBlindSetting;
    }
}
