using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PowerSourceButtonHandler : MonoBehaviour
{
    public CurrentConsumption currentConsumptionInstance;
    public ScoreManager scoreManager;

    private void Start()
    {
        Debug.Log("ButtonHandler script started.");
    }

    public void OnSolarButtonClick()
    {
        Debug.Log("Solar button clicked");
        float currentConsumption = currentConsumptionInstance.GetCurrentConsumption();
        Debug.Log("Current consumption: " + currentConsumption);

        if (currentConsumption < 10000)
        {
            Debug.Log("0 < Current Consumption < 10000, incrementing score by 1");
            scoreManager.IncrementScore(1);
        }
        else if (currentConsumption >= 10000 && currentConsumption < 20000)
        {
            Debug.Log("10000 <= Current Consumption < 20000, incrementing score by 2");
            scoreManager.IncrementScore(2);
        }
        else if (currentConsumption >= 20000 && currentConsumption < 50000)
        {
            Debug.Log("20000 <= Current Consumption < 50000, incrementing score by 5");
            scoreManager.IncrementScore(5);
        }
        else
        {
            Debug.Log("Current Consumption >= 50000, incrementing score by 10");
            scoreManager.IncrementScore(10);
        }
    }

    public void OnElectricityButtonClick()
    {
        Debug.Log("Electricity button clicked");
        float currentConsumption = currentConsumptionInstance.GetCurrentConsumption();
        Debug.Log("Current consumption: " + currentConsumption);

        if (currentConsumption < 10000)
        {
            Debug.Log("0 < Current Consumption < 10000, incrementing score by 1");
            scoreManager.IncrementScore(1);
        }
        else if (currentConsumption >= 10000 && currentConsumption < 20000)
        {
            Debug.Log("10000 <= Current Consumption < 20000, incrementing score by 1");
            scoreManager.IncrementScore(1);
        }
        else if (currentConsumption >= 20000 && currentConsumption < 50000)
        {
            Debug.Log("20000 <= Current Consumption < 50000, decrementing score by 1");
            scoreManager.DecrementScore(1);
        }
        else
        {
            Debug.Log("Current Consumption >= 50000, incrementing score by 10");
            scoreManager.DecrementScore(10);
        }
    }

    public void LoadEndScene()
    {
        int finalScore = scoreManager.GetScore();
        Debug.Log("Final Score before loading EndScene: " + finalScore);
        PlayerPrefs.SetInt("FinalScore", finalScore);
        PlayerPrefs.Save();  // Ensure the value is saved

        // Load the EndScene
        SceneManager.LoadScene("EndScene");
    }

}
