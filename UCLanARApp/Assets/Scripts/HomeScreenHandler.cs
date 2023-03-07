using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeScreenHandler : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] Button logoutButton;
    void Start()
    {
        startButton.onClick.AddListener(() => { SceneManager.LoadScene("ARScene"); });
        logoutButton.onClick.AddListener(() => { SceneManager.LoadScene("LoginScene"); });
    }
}
