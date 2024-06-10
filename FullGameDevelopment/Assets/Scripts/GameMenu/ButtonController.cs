using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public Button startGameButton; 
    public Button startQuizButton; 

    public void OnStartQuizClicked()
    {
        startGameButton.interactable = true;
    }

    public void OnStartGameClicked()
    {
        startQuizButton.onClick.RemoveListener(OnStartQuizClicked);
        SceneManager.LoadScene("StartCountDown");
    }
}
