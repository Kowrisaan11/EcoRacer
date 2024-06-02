using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public APIClient apiClient;
    public ScoreManager scoreManager;

    private void Start()
    {
        Debug.Log("ButtonHandler script started.");
    }

    public void OnSolarButtonClick()
    {
        Debug.Log("Solar button clicked");
        float currentConsumption = apiClient.GetCurrentConsumption();
        Debug.Log("Current consumption: " + currentConsumption);
        if (currentConsumption > 4000)
        {
            Debug.Log("High consumption, incrementing score by 10");
            scoreManager.IncrementScore(10);
        }
        else
        {
            Debug.Log("Low consumption, incrementing score by 5");
            scoreManager.IncrementScore(5);
        }
    }

    public void OnElectricityButtonClick()
    {
        Debug.Log("Electricity button clicked");
        float currentConsumption = apiClient.GetCurrentConsumption();
        Debug.Log("Current consumption: " + currentConsumption);
        if (currentConsumption <= 4000)
        {
            Debug.Log("Low consumption, incrementing score by 1");
            scoreManager.IncrementScore(1);
        }
        else
        {
            Debug.Log("High consumption, decrementing score by 1");
            scoreManager.DecrementScore(1);
        }
    }

    public void LoadEndScene()
    {
        int finalScore = scoreManager.GetScore();
        Debug.Log("Final Score before loading EndScene: " + finalScore);
        PlayerPrefs.SetInt("FinalScore", finalScore);

        // Load the EndScene
        SceneManager.LoadScene("EndScene");
    }

}
