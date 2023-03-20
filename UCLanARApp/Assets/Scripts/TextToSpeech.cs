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
    private List<string> locationsInfo = new List<string> { "You are not near a building on the tour! Get closer to a building and then try again!", "C And T Building. This Building is used for Computing courses, such as: Computer Science. Cyber Security. Games Development. And Software Engineering.", "Library. The Library has many lots of things to offer: 24, 7 Opening Times. Studying areas for Induviduals or Groups. 1000's of Books, Journals, and other Materials.", "Medical School. This Building offers facilities such as: Anatomage Suites. BioScience Labs. Clinical Suites. Microscopy Labs. Operating Department Suite.", "Harrington Building. This Building is used with the Medical school, but also offers things such as: The University's biggest Lecture hall, housing 400 people. Houses courses in social work and health. Large social areas to meet up and socialise", "The Student Centre. This Centre houses various things such as: Student Services. Social Areas. Events and Venues. And Rooftop Garden", "Harris Building. This is UCLAN's original, and oldest building, built in 1897. It houses the Law school and it's court room." };

    public Button readAloudButton;
    public AudioSource audioSource;

    private int counter = 0;

    void Start()
    {
        readAloudButton.onClick.AddListener(PlayAudio);
        audioSource = GetComponent<AudioSource>();
    }

    string FetchURL()
    {
        counter++;
        if (counter > 6) counter = 0;
        return "https://translate.google.com/translate_tts?ie=UTF-8&tl=en-GB&client=tw-ob&q=" + locationsInfo[counter];
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
