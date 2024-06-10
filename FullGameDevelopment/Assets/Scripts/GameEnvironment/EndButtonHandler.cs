using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndButtonHandler : MonoBehaviour
{
    public GameTimer gameTimer; // Reference to the GameTimer script
    public Button endButton; // Reference to the End button

    void Start()
    {
        endButton.onClick.AddListener(OnEndButtonClick);
    }

    void OnEndButtonClick()
    {
        gameTimer.LoadEndScene();
    }
}
