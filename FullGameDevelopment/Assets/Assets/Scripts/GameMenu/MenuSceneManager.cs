using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuSceneManager : MonoBehaviour
{

    public TextMeshProUGUI playerDisplay;
    private void Start()
    {

        if (DBManager.LoggedIn)
        {
            playerDisplay.text = "Player: " + DBManager.username;
        }

    }
    // Method to load the UpdateProfileScene
    public void OnUpdateProfileButtonClick()
    {
        SceneManager.LoadScene("UpdateProfileScene");
    }

    // Method to load the ViewProfileScene
    public void OnViewProfileButtonClick()
    {
        SceneManager.LoadScene("ViewProfileScene");
    }
}
