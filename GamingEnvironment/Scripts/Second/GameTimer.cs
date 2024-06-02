using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class Timer : MonoBehaviour
{
    public ScoreManager scoreManager;
    public TextMeshProUGUI timerText;
    public Button GameEnd; // Reference to your GameEnd button
    public float timeLimitInSeconds = 3600; // 1 hour

    private float currentTime;

    void Start()
    {

        if (SceneManager.GetSceneByName("MainScene").isLoaded)
        {
            SceneManager.UnloadSceneAsync("MainScene");
        }
        currentTime = timeLimitInSeconds;
        StartCoroutine(UpdateTimer());

        // Add click listener to the GameEnd button
        GameEnd.onClick.AddListener(EndGame);
    }

    IEnumerator UpdateTimer()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f);
            currentTime--;

            // Display timer in minutes and seconds
            float minutes = Mathf.FloorToInt(currentTime / 60);
            float seconds = Mathf.FloorToInt(currentTime % 60);

            // Update timer text
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        // Time's up, end the game
        EndGame();
    }

    void EndGame()
    {
        int finalScore = scoreManager.GetScore();
        Debug.Log("Final Score before loading EndScene: " + finalScore);
        PlayerPrefs.SetInt("FinalScore", finalScore);
        // Load the EndScene
        SceneManager.LoadScene("EndScene");
    }
}
