using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float timeLimitInSeconds = 3600; // 1 hour

    private float currentTime;

    void Start()
    {
        currentTime = timeLimitInSeconds;
        StartCoroutine(UpdateTimer());
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

        // Call TimeOut when timer reaches 0
        TimeOut();
    }

    public float GetTimeConsumed()
    {
        return timeLimitInSeconds - currentTime;
    }

    public bool IsTimeOut()
    {
        return currentTime <= 0;
    }

    public void TimeOut()
    {
        float timeConsumed = timeLimitInSeconds - currentTime;
        Debug.Log("Time Consumed: " + timeConsumed);
        PlayerPrefs.SetFloat("TimeConsumed", timeConsumed);

        // Load the EndScene
        SceneManager.LoadScene("EndScene");
    }

    public void LoadEndScene()
    {
        float timeConsumed = GetTimeConsumed();
        Debug.Log("Manual load end scene. Time Consumed: " + timeConsumed);
        PlayerPrefs.SetFloat("TimeConsumed", timeConsumed);

        // Load the EndScene
        SceneManager.LoadScene("EndScene");
    }
}
