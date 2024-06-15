using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuButtonHandler : MonoBehaviour
{
   
	public void LoadLoginScene()
    {
        SceneManager.LoadScene("LoginScene"); 
    }

    public void LoadRegisterScene()
    {
        SceneManager.LoadScene("RegisterScene"); 
    }
}
