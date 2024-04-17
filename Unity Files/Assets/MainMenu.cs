using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Quiz()
    {
        // URL of the website you want to open
        string url = "https://questionnaire-b5px.onrender.com";

        // Open the website
        OpenWebsite(url);
    }

    void OpenWebsite(string url)
    {
        Debug.Log("Opening website: " + url);
        Application.OpenURL(url);
    }
}
