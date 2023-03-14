using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Wilberforce;

public class ARScreenHandler : MonoBehaviour
{
    [SerializeField] TMP_Text exitText;
    [SerializeField] Button exitButton;
    [SerializeField] TMP_FontAsset dyslexicFont;
    [SerializeField] Camera cam;
    void Start()
    {
        if (LoginHandler.dyslexicSetting)
        {
            exitText.font = dyslexicFont;
        }
        cam.GetComponent<Colorblind>().Type = LoginHandler.colourBlindSetting;
        exitButton.onClick.AddListener(() => { SceneManager.LoadScene("HomeScreen"); });
    }
}
