using UnityEngine;
using UnityEngine.SceneManagement;

public class NextButtonHandler : MonoBehaviour
{
    // Method to be called when the button is clicked
    public void OnNextButtonClick()
    {
        // Load the MainScene
        SceneManager.LoadScene("MainScene 1");
    }
}
