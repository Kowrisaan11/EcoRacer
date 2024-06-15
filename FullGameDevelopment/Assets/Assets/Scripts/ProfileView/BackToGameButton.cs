using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToGameButton : MonoBehaviour
{
    public void OnBackToGameButtonClick()
    {
        SceneManager.LoadScene("GameMenu"); 
    }
}
