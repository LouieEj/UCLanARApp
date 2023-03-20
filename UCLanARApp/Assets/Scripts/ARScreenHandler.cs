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
    [SerializeField] TMP_Text ttsText;
    [SerializeField] TMP_Text locationText;
    [SerializeField] Button exitButton;
    [SerializeField] TMP_FontAsset dyslexicFont;
    [SerializeField] Camera cam;
    void Start()
    {
        if (LoginHandler.dyslexicSetting)
        {
            ttsText.font = dyslexicFont;
            exitText.font = dyslexicFont;
        }
        cam.GetComponent<Colorblind>().Type = LoginHandler.colourBlindSetting;
        exitButton.onClick.AddListener(() => { SceneManager.LoadScene("HomeScreen"); });
    }

}
