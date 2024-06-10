using UnityEngine;
using TMPro;

public class PlayerScoreSummary : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI timeConsumedText;

    void Start()
    {
        // Get the score value from PlayerPrefs
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);

        // Display the score value
        finalScoreText.text = "Final Score: " + finalScore;

        // Get the time consumed value from PlayerPrefs
        float timeConsumed = PlayerPrefs.GetFloat("TimeConsumed", 0);

        // Display the time consumed value
        timeConsumedText.text = "Time Consumed: " + timeConsumed + " seconds.";
        Debug.Log("Time Consumed retrieved in EndScene: " + timeConsumed);
    }
}
