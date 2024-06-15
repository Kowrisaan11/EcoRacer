using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartQuiz : MonoBehaviour
{
    public void Quiz()
    {
        // URL for the questionnaire
        string url = "https://ecoracer.onrender.com/";

        OpenWebsite(url);
    }

    void OpenWebsite(string url)
    {
        Debug.Log("Opening website: " + url);
        Application.OpenURL(url);
    }
}