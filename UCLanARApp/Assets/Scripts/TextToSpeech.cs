using ARLocation.Vendor.SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using System;
using Microsoft.Win32;
using UnityEngine.UI;

public class TextToSpeech : MonoBehaviour
{
    public static List<string[]> locations = new List<string[]>();
    private List<string> locationsInfo = new List<string> { "You are not near a building on the tour! Get closer to a building and then try again!", "C And T Building. This Building is used for Computing courses, such as: Computer Science. Cyber Security. Games Development. And Software Engineering.", "Library. The Library has many lots of things to offer: 24, 7 Opening Times. Studying areas for Induviduals or Groups. 1000's of Books, Journals, and other Materials.", "Medical School. This Building offers facilities such as: Anatomage Suites. BioScience Labs. Clinical Suites. Microscopy Labs. Operating Department Suite.", "Harrington Building. This Building is used with the Medical school, but also offers things such as: The University's biggest Lecture hall, housing 400 people. Houses courses in social work and health. Large social areas to meet up and socialise", "Harris Building. This is UCLAN's original, and oldest building, built in 1897. It houses the Law school and it's court room." };

    public Button readAloudButton;
    public AudioSource audioSource;


    void Start()
    {
        readAloudButton.onClick.AddListener(PlayAudio);
        audioSource = GetComponent<AudioSource>();
    }

    string FetchURL()
    {
        if (GameObject.Find("CTBuilding") != null)
        {
            return "https://translate.google.com/translate_tts?ie=UTF-8&tl=en-GB&client=tw-ob&q=" + locationsInfo[1];
        }
        else if (GameObject.Find("Library") != null)
        {
            return "https://translate.google.com/translate_tts?ie=UTF-8&tl=en-GB&client=tw-ob&q=" + locationsInfo[2];
        }
        else if (GameObject.Find("MedicalSchool") != null)
        {
            return "https://translate.google.com/translate_tts?ie=UTF-8&tl=en-GB&client=tw-ob&q=" + locationsInfo[3];
        }
        else if (GameObject.Find("HarringtonBuilding") != null)
        {
            return "https://translate.google.com/translate_tts?ie=UTF-8&tl=en-GB&client=tw-ob&q=" + locationsInfo[4];
        }
        else if (GameObject.Find("HarrisBuilding") != null)
        {
            return "https://translate.google.com/translate_tts?ie=UTF-8&tl=en-GB&client=tw-ob&q=" + locationsInfo[5];
        }
        else
        {
            return "https://translate.google.com/translate_tts?ie=UTF-8&tl=en-GB&client=tw-ob&q=" + locationsInfo[0];
        }
    }


    void DownloadAudio(string url)
    {
        WWW www = new WWW(url);

        audioSource.clip = www.GetAudioClip(false, true, AudioType.MPEG);
        audioSource.Play();
    }

    void PlayAudio()
    {
        DownloadAudio(FetchURL());
    }
}
