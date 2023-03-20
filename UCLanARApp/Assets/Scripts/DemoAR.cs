using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using TMPro;
using Wilberforce;

public class DemoAR : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToLoad;
    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] TMP_FontAsset dyslexicFont;
    [SerializeField] Button exitButton;
    [SerializeField] Camera cam;

    private void Start()
    {
        if (LoginHandler.dyslexicSetting)
        {
            exitButton.GetComponentInChildren<TMP_Text>().font = dyslexicFont;
        }
        cam.GetComponent<Colorblind>().Type = LoginHandler.colourBlindSetting;
        exitButton.onClick.AddListener(() => { SceneManager.LoadScene("HomeScreen"); });
    }

    private int objectCounter = 0;

    // Update is called once per frame
    void Update()
    {
        Vector2 screenPos = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenPos, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        if (hits.Count > 0 )
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (objectCounter < objectsToLoad.Length)
            {
                objectsToLoad[objectCounter].gameObject.transform.localScale = new Vector3(0.15f, 0.15f, 0.05f);
                GameObject.Instantiate(objectsToLoad[objectCounter], transform.position, transform.rotation);
                objectCounter += 1;
            }
        }


    }
}
