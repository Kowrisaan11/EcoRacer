// BacktoMainMenuButton.cs
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BacktoMainMenuButton : MonoBehaviour
{
    public APIProfileRegister APIProfileRegister;
    public Button MainMenuButton;

    public void OnBacktoMainMenuButtonClick()
    {
        SceneManager.LoadScene("MainMenu");
    }

    
}
